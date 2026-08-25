using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Npc.ConcreateClass;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
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

        [Inject]
        void Construct( BarService barService, SeatService seatService ) {
            this.barService = barService;
            this.seatService = seatService;
        }

        void Awake() {
            npcInfo = new NPCInfo(); //later will need another script for this


            moveScript = new NPCMoveScript(this.gameObject.transform, machineState);
            waitScript = new NPCWaitingScript(machineState);
            consumeOrder = new NPCConsumeOrder(this);

            //machineState.Initialize(moveScript);
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

        public void Leave() {
            moveScript.SetDestination(new Vector3(-10, -0.5f, 5));
            machineState.ChangeState(moveScript);
        }

    }
}