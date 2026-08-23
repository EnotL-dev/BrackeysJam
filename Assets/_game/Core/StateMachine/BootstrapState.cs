using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.StateMachine
{
    public class BootstrapState : IGameState
    {
        private readonly IGameStateMachine stateMachine;

        public BootstrapState(IGameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

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