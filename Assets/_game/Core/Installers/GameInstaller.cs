using Assets._game.Core.Bootstrap;
using Assets._game.Core.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindStates();

            BindBootstrap();
        }

        private void BindStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                     .To<GameStateMachine>()
                     .AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle().NonLazy();
        }
    }
}