using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets._game.Npc.ConcreateClass {
    public class NPCMoveScript : INPCState {

        readonly NPCMachineState machineState;

        Transform transform;

        float timeMoving = 5;
        
        Vector3 dest;

        public NPCMoveScript( Transform transform, NPCMachineState machineState ) {
            this.transform = transform;
            this.machineState = machineState;
        }

        public void SetDestination( Vector3 pos) {
            dest = pos;
        }



        public void EnterState() {
            //use ai navigation for this

            //For testing

            //Debug.Log($"move this npc to some {dest}");
            transform.DOMove(dest, timeMoving);
        }

        public void ExitState() {
            //throw new System.NotImplementedException();

            //Debug.Log("exit move");
        }

        public void UpdateState() {
            throw new System.NotImplementedException();
        }
    }
}