using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._game.Core.StateMachine
{
    public interface IGameState
    {
        UniTask Enter();
        UniTask Exit();
    }
}
