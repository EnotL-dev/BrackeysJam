using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Assets._game.Core.StateMachine
{
    public class NightShiftState : IGameState
    {

        public async UniTask Enter()
        {
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
    }
}