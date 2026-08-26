using Assets._game.Store.Controller;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class StoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStore();
        }

        private void BindStore()
        {
            Container.Bind<IStoreService>()
                     .To<StoreService>()
                     .AsSingle();
        }
    }
}