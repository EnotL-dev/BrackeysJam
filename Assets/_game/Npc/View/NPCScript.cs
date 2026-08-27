using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Npc.Animation;
using Assets._game.Npc.ConcreateClass;
using Assets._game.Npc.Enum;
using System;
using System.Collections;
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
        [SerializeField] private float maxWaitDrinkTimeout = 60f;
        [SerializeField] private float checkDrinkInterval = 0.5f;


        BarService barService;
        SeatService seatService;
        OrderFactory orderFactory;
        public AlcoholCatalog alcoholCatalog { get; private set; }
        public NavMeshAgent agent { get; private set; }

        [Inject]
        void Construct( BarService barService,
            SeatService seatService,
            OrderFactory orderFactory,
            AlcoholCatalog alcoholCatalog ) {
            this.barService = barService;
            this.seatService = seatService;
            this.orderFactory = orderFactory;
            this.alcoholCatalog = alcoholCatalog;
        }

        void Awake() {
            Animator animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            moveScript = new NPCMoveScript(this);
            waitScript = new NPCWaitingScript(machineState);
            consumeOrder = new NPCConsumeOrder(this);

            animationController = new NPCAnimationController(this);
        }

        public void Start() {

            machineState.Initialize(moveScript);

        }

        public void Initialize( NPCInfo info ) {
            npcInfo = info;
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
        }


        public void MoveToDest( Vector3 pos ) {
            moveScript.SetDestination(pos);
            machineState.ChangeState(moveScript);
        }




        //public void MoveToWaitingLine( Vector3 pos ) {
        //    moveScript.SetDestination(transform.position);
        //    machineState.ChangeState(moveScript);
        //}

        public void MoveToBar( Vector3 pos1, Vector3 pos2 ) {
            moveScript.SetDestination(pos1);
            machineState.ChangeState(moveScript, () => {
                Debug.Log("Moving to bar Done");

                if ( waitForDrinkCoroutine != null ) StopCoroutine(waitForDrinkCoroutine);

                waitForDrinkCoroutine = StartCoroutine(WaitForAvailableDrinkRoutine(pos2));
            });


        }

        private IEnumerator WaitForAvailableDrinkRoutine( Vector3 seatPos ) {
            float elapsed = 0f;

            // Optional: Switch to an idle/waiting animation state while waiting at the counter
            // machineState.ChangeState(waitScript);

            while ( elapsed < maxWaitDrinkTimeout ) {
                var order = orderFactory.GetRandomOrder();
                if ( order != null ) {
                    Debug.Log("Drink restocked, placing order.");
                    waitForDrinkCoroutine = null;
                    PlaceOrder(seatPos, order);
                    yield break;
                }

                Debug.Log($"Waiting for drinks to be stocked... ({elapsed:F1}s/{maxWaitDrinkTimeout}s)");
                yield return new WaitForSeconds(checkDrinkInterval);
                elapsed += checkDrinkInterval;
            }

            // Patience ran out because the player never restocked
            Debug.Log("NPC waited too long for a drink and is leaving.");
            waitForDrinkCoroutine = null;

            // Optional: Add negative bar vibe/reputation penalty here
            // barService.ReduceVibe(5);

            Leave();
        }


        public void PlaceOrder( Vector3 pos, Order order = null ) {
            //machineState.ChangeState(waitScript);

            Debug.Log("Calling for order");

            StartCoroutine(barService.RequestDrink((AlcoholOrder)order, () => {
                MoveToDest(pos);
                machineState.ChangeState(moveScript, () => {

                    consumeOrder.ChangeAlcoholSO(order);
                    machineState.ChangeState(consumeOrder, () => {
                        Leave();
                    });
                });

            }));


        }

        public void ConsumeOrder( Order oder ) {
            //consumeOrder.ChangeAlcoholSO(oder);
            machineState.ChangeState(consumeOrder); //TODO: make the method use the second
            //animationController.SetAction(NPCActionState.ConsumeOrder);
        }

        public void WaitForConsumeOrder( float seconds, Action onComplete ) {
            StartCoroutine(WaitForConsumeOrderRoutine(seconds, onComplete));

        }

        private IEnumerator WaitForConsumeOrderRoutine(
            float seconds,
            Action onComplete ) {
            yield return new WaitForSeconds(seconds);

            onComplete?.Invoke();
        }

        //TODO: reafactor this into a real point instead of hardcode
        public void Leave() {
            if ( npcInfo.npcProperties == (NPCProperty.HotTemper) ) {
                barService.ReduceChaos(0.1f);
            }

            Vector3 destination = new Vector3(50, -0.5f, 55); // Ref a real postion and handle destory


            MoveToDest(destination);
        }

        public void SitDown() {
            //animationController.SetAction(NPCActionState.Sit);
        }

        public void StandUP() {
            //animationController.SetAction(NPCActionState.StandUp);
        }




    }
}