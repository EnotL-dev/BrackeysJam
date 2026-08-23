using Assets._game.Bar.Controller;
using Assets._game.Player.Controller;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallInteraction();

            Debug.Log("<color=green>Player was initialized</color>");
        }

        private void InstallInteraction()
        {
            Container.Bind<IPlayerInteractionService>()
                     .To<PlayerInteractionService>()
                     .AsSingle();
        }
    }
}