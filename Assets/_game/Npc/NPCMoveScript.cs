using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCMoveScript : INPCState {

        readonly NPCMachineState machineState;

        Transform transform;


        public NPCMoveScript( Transform transform, NPCMachineState machineState) {
            this.transform = transform;
            this.machineState = machineState;
        }


        public void EnterState() {
            //use ai navigation for this

            //For testing

            Debug.Log("move this npc to some where");

            var temp = new Vector3 ( 10,0,10);

            transform.DOMove(temp, 10);
        }

        public void ExitState() {
            throw new System.NotImplementedException();
        }

        public void UpdateState() {
            throw new System.NotImplementedException();
        }
    }
}