using Assets._game.UI;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers {
    public class SettingInstaller : MonoInstaller {

        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<SettingsService>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}