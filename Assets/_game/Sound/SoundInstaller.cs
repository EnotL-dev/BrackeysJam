using Assets._game.Sound.Controller;
using Assets._game.Sound.EnumInterface;
using Assets._game.Sound.SO;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Sound {
    public class SoundInstaller : MonoInstaller {

        [SerializeField] private MusicConfigSO musicConfig;
        [SerializeField] private SFXConfigSO sfxConfig;
        
        public override void InstallBindings() {
            Container.BindInstance(musicConfig);
            Container.BindInstance(sfxConfig);

            Container.Bind<IMusicService>()
                .To<MusicService>()
                .AsSingle();

            Container.Bind<ISFXService>()
                .To<SFXService>()
                .AsSingle();
        }
    }
}