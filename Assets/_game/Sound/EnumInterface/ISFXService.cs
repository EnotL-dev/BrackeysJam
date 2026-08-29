using FMOD.Studio;
using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.EnumInterface {
    public interface ISFXService {
        void Play( SFXType type );
        void PlayInSpace( SFXType type, Vector3 pos );
        EventInstance StartLoop( SFXType type, GameObject gameObject = null );
        void StopLoop( EventInstance instance );
    }
}