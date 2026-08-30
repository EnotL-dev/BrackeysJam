using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Core.StateMachine;
using Assets._game.Npc.Animation;
using Assets._game.Npc.ConcreateClass;
using Assets._game.Npc.Enum;
using Assets._game.NpcGenerator.View;
using Assets._game.Sound.Controller;
using Assets._game.Sound.EnumInterface;
using DG.Tweening;
using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

namespace Assets._game.Npc.View {
    public class NPCScript : MonoBehaviour {

        readonly NPCMachineState machineState = new NPCMachineState(); //might use DI

        public NPCInfo npcInfo;
        private Coroutine waitForDrinkCoroutine;

        public NPCAnimationController animationController { get; private set; }

        public NPCMoveScript moveScript;
        public NPCWaitingScript waitScript;
        public NPCConsumeOrder consumeOrder;


        [SerializeField] GameObject LeavePos;
        [SerializeField] private float maxWaitServeDrinkTimeout = 60f;
        [SerializeField] private float checkDrinkInterval = 0.5f;


        BarService barService;
        ISeatService seatService;
        OrderFactory orderFactory;
        ISFXService sFXService;
        WorldSettingScript worldSettingScript;


        public AlcoholCatalog alcoholCatalog { get; private set; }
        public NavMeshAgent agent { get; private set; }
        public Animator animator { get; private set; }

        SignalBus signalBus;

        [Inject]
        void Construct( BarService barService,
            ISeatService seatService,
            OrderFactory orderFactory,
            AlcoholCatalog alcoholCatalog,
            ISFXService sFXService,
            SignalBus signalBus,
            WorldSettingScript worldSettingScript ) {
            this.barService = barService;
            this.seatService = seatService;
            this.orderFactory = orderFactory;
            this.alcoholCatalog = alcoholCatalog;
            this.sFXService = sFXService;
            this.signalBus = signalBus;
            this.worldSettingScript = worldSettingScript;
        }

        void Awake() {

            agent = GetComponent<NavMeshAgent>();

            moveScript = new NPCMoveScript(this);
            waitScript = new NPCWaitingScript(machineState);
            consumeOrder = new NPCConsumeOrder(this);

            animationController = new NPCAnimationController(this);
        }

        public void Start() {
            machineState.Initialize(moveScript);
        }

        private void OnEnable() {
            signalBus.Subscribe<StateChangedSignal>(StateChanged);
        }

        private void OnDisable() {
            signalBus.Unsubscribe<StateChangedSignal>(StateChanged);
        }

        public void StateChanged( StateChangedSignal stateChangedSignal ) {
            if ( stateChangedSignal.gameState is DayShiftState ) {
                Leave();
            }
        }

        public void Initialize( NPCInfo info ) {
            npcInfo = info;
        }

        public void SetCharacterModel( NpcGeneratedCharacterView characterView ) {
            animator = characterView.GetComponentInChildren<Animator>(true);

            if ( animator == null ) {
                Debug.LogError("No Animator found on generated character prefab!", characterView);
            }
        }

        public void ChangeState( NPCState state ) {
            switch ( state ) {
                case NPCState.MoveToLine:
                case NPCState.MoveToBar:

                    machineState.ChangeState(moveScript);
                    break;


                case NPCState.Left:
                    machineState.ChangeState(moveScript);
                    break;




                default:
                    Debug.LogWarning("if you see this log then there might be broken in npc change state");
                    break;

            }
        }

        public void Update() {
            machineState.UpdateState();
            animationController.UpdateAnimation();
        }

        #region Move

        public void MoveToDest( Vector3 pos, NPCMovementOwner owner = NPCMovementOwner.None ) {

            moveScript.TrySetDestination(pos, owner);

            machineState.ChangeState(moveScript, () => {
            });
        }

        public void ReOrganizeInLine( Vector3 pos ) {
            if ( moveScript.TrySetDestination(pos, NPCMovementOwner.WaitingLine) ) {
                machineState.ChangeState(moveScript);
            }
        }

        public void MoveToBar( Vector3 pos1, Seat seat ) {
            moveScript.TrySetDestination(pos1, NPCMovementOwner.Action);
            machineState.ChangeState(moveScript, () => {
                Debug.Log("Moving to bar Done");
                OrderDrink(seat);
            });
        }

        public void MoveToSeat( Vector3 pos, Quaternion rotation, Action onComplete ) {
            moveScript.TrySetDestination(pos, NPCMovementOwner.Action);
            machineState.ChangeState(moveScript, () => {
                transform.rotation = rotation;
                onComplete?.Invoke();
            });
        }

        #endregion


        //public void MoveToWaitingLine( Vector3 pos ) {
        //    moveScript.SetDestination(transform.position);
        //    machineState.ChangeState(moveScript);
        //}



        private void OrderDrink( Seat seat ) {
            if ( waitForDrinkCoroutine != null ) StopCoroutine(waitForDrinkCoroutine);
            waitForDrinkCoroutine = StartCoroutine(WaitForAvailableDrinkRoutine(seat));
        }

        private IEnumerator WaitForAvailableDrinkRoutine( Seat seat ) {
            float elapsed = 0f;

            // Optional: Switch to an idle/waiting animation state while waiting at the counter
            // machineState.ChangeState(waitScript);

            while ( elapsed < maxWaitServeDrinkTimeout ) {
                var order = orderFactory.GetRandomOrder();
                if ( order == null ) {
                    Debug.Log($"Waiting for drinks to be stocked... ({elapsed:F1}s/{maxWaitServeDrinkTimeout}s)");
                    yield return new WaitForSeconds(checkDrinkInterval);
                    elapsed += checkDrinkInterval;
                }

                Debug.Log("Drink restocked, placing order.");
                waitForDrinkCoroutine = null;

                PlaceOrder(seat, order);

                yield break;
            }

            // Patience ran out because the player never restocked
            Debug.Log("NPC waited too long for a drink and is leaving.");
            waitForDrinkCoroutine = null;

            // Optional: Add negative bar vibe/reputation penalty here
            // barService.ReduceVibe(5);

            Leave();
        }


        public void PlaceOrder( Seat seat, Order order = null ) {
            //machineState.ChangeState(waitScript);

            Debug.Log("Calling for order");

            int amount = npcInfo.wealth switch {
                NPCWealthType.Poor => 1,
                NPCWealthType.Normal => UnityEngine.Random.Range(2, 4), // 2 or 3
                NPCWealthType.Rich => UnityEngine.Random.Range(3, 5),   // 3 or 4
                _ => 1
            };


            barService.RequestDrink((AlcoholOrder)order, () => {

                //move to seat
                MoveToSeat(seat.SitPosition(), seat.SitRotation(), () => {
                    //on reach seat sit down
                    animationController.SetAction(NPCActionState.Sit);

                    //consume order
                    consumeOrder.ChangeAlcoholSO(order);
                    machineState.ChangeState(consumeOrder, () => {
                        //stand up leave
                        animationController.SetAction(NPCActionState.StandUp);
                        Leave();
                    });
                });
            });

        }

        public void ConsumeOrder( Order oder ) {
            //consumeOrder.ChangeAlcoholSO(oder);
            machineState.ChangeState(consumeOrder); //TODO: make the method use the second
            //animationController.SetAction(NPCActionState.ConsumeOrder);
        }

        public void WaitForConsumeOrder( float seconds, Action onComplete ) {
            sFXService.PlayInSpace(SFXType.NPCDrink, gameObject.transform.position);
            animationController.SetAction(NPCActionState.ConsumeOrder);

            StartCoroutine(WaitForConsumeOrderRoutine(seconds, onComplete));

        }

        private IEnumerator WaitForConsumeOrderRoutine(
            float seconds,
            Action onComplete ) {
            sFXService.PlayInSpace(SFXType.NPCDrink, gameObject.transform.position);

            yield return new WaitForSeconds(seconds);

            onComplete?.Invoke();
        }

        //TODO: reafactor this into a real point instead of hardcode
        public void Leave() {
            if ( npcInfo.npcProperties == (NPCProperty.HotTemper) ) {
                barService.ReduceChaos(0.1f);
            }

            Vector3 destination = worldSettingScript.GetLeavePoint(); // Ref a real postion and handle destory
            moveScript.TrySetDestination(destination, NPCMovementOwner.Action);
            machineState.ChangeState(moveScript, () => { Destroy(gameObject); });
        }

        public void ForceLeave() {
            Vector3 destination = worldSettingScript.GetLeavePoint(); // Ref a real postion and handle destory
            moveScript.TrySetDestination(destination, NPCMovementOwner.Action);
            machineState.ChangeState(moveScript, () => {
                Destroy(gameObject);
            });
        }

        public void SitDown() {
            //animationController.SetAction(NPCActionState.Sit);
        }

        public void StandUp() {
            //animationController.SetAction(NPCActionState.StandUp);
        }

        public void StopAllBehaviour() {
            moveScript.SetAgentEnabled(false);
            machineState.ChangeState(waitScript);
        }

        public void RecoverFromKnockOut() {
            moveScript.SetAgentEnabled(true);
            Leave();
        }

        private void OnDrawGizmos() {
            if ( agent == null || agent.path == null || agent.path.corners.Length < 2 )
                return;

            Gizmos.color = Color.red;
            Vector3[] corners = agent.path.corners;

            for ( int i = 0; i < corners.Length - 1; i++ ) {
                // Draw lines between each waypoint corner
                Gizmos.DrawLine(corners[i], corners[i + 1]);
                // Draw a small sphere at each corner/turn point
                Gizmos.DrawSphere(corners[i + 1], 0.15f);
            }
        }




    }
}