using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Npc.Animation;
using Assets._game.Npc.ConcreateClass;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets._game.Npc
{
    public class NPCScript : MonoBehaviour
    {

        readonly NPCMachineState machineState = new NPCMachineState(); //might use DI

        public NPCInfo npcInfo;

        public NPCAnimationController animationController { get; private set; }

        public NPCMoveScript moveScript;
        public NPCWaitingScript waitScript;
        public NPCConsumeOrder consumeOrder;


        [SerializeField] GameObject LeavePos;


        BarService barService;
        SeatService seatService;
        OrderFactory orderFactory;

        public NavMeshAgent agent { get; private set; }

        [Inject]
        void Construct(BarService barService,
            SeatService seatService,
            OrderFactory orderFactory)
        {
            this.barService = barService;
            this.seatService = seatService;
            this.orderFactory = orderFactory;
        }

        void Awake()
        {
            Animator animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            npcInfo = new NPCInfo(); //later will need another script for this

            moveScript = new NPCMoveScript(this);
            waitScript = new NPCWaitingScript(machineState);
            consumeOrder = new NPCConsumeOrder(this);

            animationController = new NPCAnimationController(this);
        }

        public void Start()
        {

            machineState.Initialize(moveScript);

        }

        public void ChangeState(NPCState state)
        {
            switch (state)
            {
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

        public void Update()
        {
            machineState.UpdateState();
        }


        public void MoveToDest(Vector3 pos)
        {
            moveScript.SetDestination(pos);
            machineState.ChangeState(moveScript);
        }




        //public void MoveToWaitingLine( Vector3 pos ) {
        //    moveScript.SetDestination(transform.position);
        //    machineState.ChangeState(moveScript);
        //}

        public void MoveToBar(Vector3 pos)
        {
            moveScript.SetDestination(pos);
            machineState.ChangeState(moveScript, () => {
                var order = orderFactory.GetRandomOrder();
                if (order == null)
                {
                    Debug.LogWarning("THIS IS A BUG OF PLACING ORDER");
                    return;
                }

                PlaceOrder(order);
            });


        }


        public void PlaceOrder(Order order = null)
        {
            //machineState.ChangeState(waitScript);

            Debug.Log("Calling for order");

            StartCoroutine(barService.RequestDrink((AlcoholOrder)order, () => {
                var pos = seatService.FindBestSeat(transform.position);
                MoveToDest(pos.transform.position);
                machineState.ChangeState(moveScript, () => {

                    machineState.ChangeState(consumeOrder, () => {

                        Leave();
                    });
                });

            }));


        }

        public void ConsumeOrder(float second)
        {
            machineState.ChangeState(consumeOrder); //TODO: make the method use the second
            //animationController.SetAction(NPCActionState.ConsumeOrder);
        }

        public void WaitForConsumeOrder(float seconds, Action onComplete)
        {
            StartCoroutine(WaitForConsumeOrderRoutine(seconds, onComplete));

        }

        private IEnumerator WaitForConsumeOrderRoutine(
            float seconds,
            Action onComplete)
        {
            yield return new WaitForSeconds(seconds);

            onComplete?.Invoke();
        }

        //TODO: reafactor this into a real point instead of hardcode
        public void Leave()
        {
            moveScript.SetDestination(new Vector3(65, -0.5f, 50));
            machineState.ChangeState(moveScript);
        }

        public void SitDown()
        {
            //animationController.SetAction(NPCActionState.Sit);
        }

        public void StandUP()
        {
            //animationController.SetAction(NPCActionState.StandUp);
        }




    }
}