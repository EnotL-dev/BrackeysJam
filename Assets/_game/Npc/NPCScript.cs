using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCScript : MonoBehaviour {

        readonly NPCMachineState machineState = new NPCMachineState(); //might use DI
        
        public NPCMoveScript moveScript;
        public NpcWaitingScript waitScript;

        void Awake() {
            moveScript = new NPCMoveScript(this.gameObject.transform, machineState);
            waitScript = new NpcWaitingScript(machineState);

            //machineState.Initialize(moveScript);
        }

        public void ChangeState(NPCState state) {
            switch ( state ) {
                case NPCState.MoveToLine:
                    machineState.ChangeState(waitScript);
                    break;

                case NPCState.Left:
                    machineState.ChangeState(moveScript);
                    break;




                default:
                    Debug.LogWarning("if you see this log then there might be broken in npc change state");
                    break;

            }
        }

        public void MoveToWaitingLine(Transform transform) {
            moveScript.SetDestination(transform);
            machineState.ChangeState(moveScript);
        }


    }
}