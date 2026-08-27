using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.SO {
    [CreateAssetMenu(fileName = "AudioSO", menuName = "AudioSO/SFX Config")]
    public class SFXConfigSO : ScriptableObject {

        public EventReference npcDrink;
        public EventReference bartenderPourBeer;
        public EventReference cashIn;
        public EventReference npcSpeech;
    }
}