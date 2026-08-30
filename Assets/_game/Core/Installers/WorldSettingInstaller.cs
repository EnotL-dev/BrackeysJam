using Assets._game.Npc;
using Assets._game.Store.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers {
    public class WorldSettingInstaller : MonoInstaller {

        [SerializeField] WorldSettingScript script;


        public override void InstallBindings() {
            BindStore();
        }

        private void BindStore() {
            Container.Bind<WorldSettingScript>()
                .FromInstance(script)
                .AsSingle();
        }


    }
}