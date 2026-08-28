using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.SO {
    [CreateAssetMenu(fileName = "MusicSO", menuName = "AudioSO/Music Config")]
    public class MusicConfigSO : ScriptableObject {

        public EventReference uiMusic;
        public EventReference dayMusic;
        public EventReference nightMusic;
    }
}