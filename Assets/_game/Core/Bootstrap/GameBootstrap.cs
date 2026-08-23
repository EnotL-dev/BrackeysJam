using Assets._game.Core.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Bootstrap
{
    public class GameBootstrap : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        public GameBootstrap(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            Debug.Log("<color=green>GameBootstrap initialized</color>");

            Application.targetFrameRate = 60;
            _stateMachine.Enter<BootstrapState>();
        }
    }
}
