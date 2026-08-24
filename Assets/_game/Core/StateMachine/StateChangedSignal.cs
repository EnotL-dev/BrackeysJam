using System.Collections;
using UnityEngine;

namespace Assets._game.Core.StateMachine
{
    public class StateChangedSignal
    {
        public IGameState gameState { get; }

        public StateChangedSignal(IGameState gameState)
        {
            this.gameState = gameState;
        }
    }
}