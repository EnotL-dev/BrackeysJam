using Assets._game.Sound.EnumInterface;
using Assets._game.Sound.SO;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.Controller {
    public class MusicService : IMusicService {

        private readonly MusicConfigSO config;

        private EventInstance currentMusic;
        private MusicType? currentType;

        public MusicService( MusicConfigSO config ) {
            this.config = config;
        }

        public void Play( MusicType type ) {
            if ( currentType == type ) return;

            Stop();

            EventReference reference = type switch
        {
            MusicType.UI => config.uiMusic,
            MusicType.Day => config.dayMusic,
            MusicType.Night => config.nightMusic,
            _ => default
        };

            currentMusic = RuntimeManager.CreateInstance(reference);

            currentMusic.start();

            currentType = type;
        }

        public void Stop() {
            if ( !currentMusic.isValid() )
                return;

            currentMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentMusic.release();

            currentMusic = default;
            currentType = null;
        }
    }
}