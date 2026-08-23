using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._game.Core.StateMachine
{
    public interface IGameStateMachine
    {
        UniTask Enter<TState>() where TState : IGameState;
    }
}
