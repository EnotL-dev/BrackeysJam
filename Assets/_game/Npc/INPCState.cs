using System;
using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public interface INPCState {

        void EnterState(Action onComplete);
        void ExitState(); 
        void UpdateState(); //might dont need
    }
}