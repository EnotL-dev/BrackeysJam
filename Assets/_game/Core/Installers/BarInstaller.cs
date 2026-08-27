using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Bar.View;
using Assets._game.Player.View;
using Assets._game.Shift.Controller;
using System;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers {
    public class BarInstaller : MonoInstaller {

        [SerializeField] private DeskManagerView deskManagerView;

        public override void InstallBindings() {
            BindEconomy();
            BindBar();
            BindDesk();

            Debug.Log("<color=green>Bar was initialized</color>");
        }

        private void BindEconomy()
        {
            Container.Bind<IEconomyService>()
                 .To<EconomyService>()
                 .AsSingle();
        }

        private void BindBar() {

            Container.Bind<AlcoholCatalog>()
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesAndSelfTo<BarService>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindDesk()
        {
            Container.Bind<DeskManagerView>()
                 .FromComponentOn(deskManagerView.gameObject)
                 .AsSingle();
        }
    }
}