using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCMachineState {

        public INPCState currentState { get; private set; }


        public void Initialize(INPCState move) {
            //init
            currentState = move;
            currentState?.EnterState();
        }


        public void ChangeState( INPCState newState ) {
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState();
        }

        private void UpdateState() {
            currentState?.UpdateState();
        }

    }
}