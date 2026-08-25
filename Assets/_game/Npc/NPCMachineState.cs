using System;
using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCMachineState {

        public INPCState currentState { get; private set; }


        public void Initialize( INPCState move, Action onComplete = null ) {
            //init
            currentState = move;
            currentState?.EnterState(onComplete);
        }


        public void ChangeState( INPCState newState, Action onComplete = null ) {
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState(onComplete);
        }

        public void UpdateState() {
            currentState.UpdateState();
        }




    }
}