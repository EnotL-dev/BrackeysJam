using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using System;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers {
    public class BarInstaller : MonoInstaller {

        public override void InstallBindings() {
            BindBar();

            Debug.Log("<color=green>Bar was initialized</color>");
        }

        private void BindBar() {

            Container.Bind<AlcoholCatalog>()
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesAndSelfTo<BarService>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IEconomyService>()
                     .To<EconomyService>()
                     .AsSingle();
        }
    }
}