using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCScript : MonoBehaviour {

        readonly NPCMachineState machineState = new NPCMachineState(); //might use DI

        public NPCInfo npcInfo;

        public NPCMoveScript moveScript;
        public NpcWaitingScript waitScript;

        void Awake() {
            npcInfo = new NPCInfo(); //later will need another script for this


            moveScript = new NPCMoveScript(this.gameObject.transform, machineState);
            waitScript = new NpcWaitingScript(machineState);

            //machineState.Initialize(moveScript);
        }

        public void ChangeState(NPCState state) {
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


        public void MoveToDest(Transform transform) {
            moveScript.SetDestination(transform);
            machineState.ChangeState(moveScript);
        }

        
        public void MoveToWaitingLine(Transform transform) {
            moveScript.SetDestination(transform);
            machineState.ChangeState(moveScript);
        }

        public void MoveToBar(Transform transform) {
            moveScript.SetDestination(transform);
            machineState.ChangeState(moveScript);
        }



    }
}