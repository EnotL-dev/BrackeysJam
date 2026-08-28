using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.EnumInterface {
    public interface ISFXService {
        void Play( SFXType type);
        void PlayInSpace( SFXType type, Vector3 pos);
    }
}