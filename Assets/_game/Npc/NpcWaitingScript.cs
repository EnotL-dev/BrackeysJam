using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NpcWaitingScript : INPCState {

        readonly NPCMachineState machineState;

        //wait 
        //after a period of time left?.

        public NpcWaitingScript(NPCMachineState NPCMachineState ) {
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