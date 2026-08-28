using Assets._game.Store.View;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class StoreInstaller : MonoInstaller
    {
        [SerializeField] private FurnitureManagerView furnitureManagerView;
        [SerializeField] private StoreView storeView;

        public override void InstallBindings()
        {
            BindStore();
        }

        private void BindStore()
        {
            Container.Bind<StoreView>()
                 .FromComponentOn(storeView.gameObject)
                 .AsSingle();

            Container.Bind<FurnitureManagerView>()
                 .FromComponentOn(furnitureManagerView.gameObject)
                 .AsSingle();
        }
    }
}