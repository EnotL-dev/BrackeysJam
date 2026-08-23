using Assets._game.Bar.Controller;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class BarInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBar();

            Debug.Log("<color=green>Bar was initialized</color>");
        }

        private void BindBar()
        {
            Container.Bind<IBarService>()
                     .To<BarService>()
                     .AsSingle();
        }
    }
}