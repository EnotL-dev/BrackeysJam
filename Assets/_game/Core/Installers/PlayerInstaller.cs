using Assets._game.Bar.Controller;
using Assets._game.Player.Controller;
using Assets._game.Player.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInterfaceManagerView playerInterfaceManagerView;
        [SerializeField] private PlayerInteractionView playerInteractionView;
        [SerializeField] private ArmsAnimatorView armsAnimatorView;

        public override void InstallBindings()
        {
            BindInteraction();
            BindPlayerInterface();

            Debug.Log("<color=green>Player was initialized</color>");
        }

        private void BindInteraction()
        {
            Container.Bind<ArmsAnimatorView>()
                 .FromComponentOn(armsAnimatorView.gameObject)
                 .AsSingle();

            Container.Bind<IPlayerInteractionService>()
                     .To<PlayerInteractionService>()
                     .AsSingle();
        }
        private void BindPlayerInterface()
        {
            Container.Bind<CameraShakingView>()
                 .FromComponentOn(playerInteractionView.gameObject)
                 .AsSingle();

            Container.Bind<PlayerInterfaceManagerView>()
                 .FromComponentOn(playerInterfaceManagerView.gameObject)
                 .AsSingle();

            Container.Bind<PlayerInteractionView>()
                 .FromComponentOn(playerInteractionView.gameObject)
                 .AsSingle();
        }
    }
}