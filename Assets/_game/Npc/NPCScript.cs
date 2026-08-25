using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Npc.ConcreateClass;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets._game.Npc {
    public class NPCScript : MonoBehaviour {

        readonly NPCMachineState machineState = new NPCMachineState(); //might use DI

        public NPCInfo npcInfo;

        public NPCMoveScript moveScript;
        public NPCWaitingScript waitScript;
        public NPCConsumeOrder consumeOrder;


        [SerializeField] GameObject LeavePos;


        BarService barService;
        SeatService seatService;

        public NavMeshAgent agent { get; private set; }

        [Inject]
        void Construct( BarService barService, SeatService seatService ) {
            this.barService = barService;
            this.seatService = seatService;
        }

        void Awake() {
            npcInfo = new NPCInfo(); //later will need another script for this


            moveScript = new NPCMoveScript(this);
            waitScript = new NPCWaitingScript(machineState);
            consumeOrder = new NPCConsumeOrder(this);


            agent = GetComponent<NavMeshAgent>();
        }

        public void Start() {

            machineState.Initialize(moveScript);

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

        public void MoveToBar( Vector3 pos ) {
            moveScript.SetDestination(pos);
            machineState.ChangeState(moveScript, () => {
                PlaceOrder();
            });


        }


        public void PlaceOrder( Order order = null ) {
            //machineState.ChangeState(waitScript);

            StartCoroutine(barService.RequestOrder(this, order, () => {
                var pos = seatService.FindBestSeat();
                MoveToDest(pos.transform.position);
                machineState.ChangeState(moveScript, () => {

                    machineState.ChangeState(consumeOrder, () => {

                        Leave();
                    });
                });

            }));


        }

        public void ConsumeOrder( float second ) {
            machineState.ChangeState(consumeOrder); //TODO: make the method use the second
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
            moveScript.SetDestination(new Vector3(65, -0.5f, 50));
            machineState.ChangeState(moveScript);
        }

    }
}