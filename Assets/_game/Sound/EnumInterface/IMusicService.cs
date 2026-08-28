using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.EnumInterface {
    public interface IMusicService {

        void Play( MusicType type );
        void Stop();
    }
}