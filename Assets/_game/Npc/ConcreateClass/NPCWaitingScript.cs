using System.Collections;
using UnityEngine;

namespace Assets._game.Npc.ConcreateClass {
    public class NPCWaitingScript : INPCState {

        readonly NPCMachineState machineState;

        //wait 
        //after a period of time left?.

        public NPCWaitingScript(NPCMachineState NPCMachineState ) {
            machineState = NPCMachineState;
        }

        public void EnterState() {
            throw new System.NotImplementedException();
        }

        public void ExitState() {
            throw new System.NotImplementedException();
        }

        public void UpdateState() {
            throw new System.NotImplementedException();
        }
    }
}