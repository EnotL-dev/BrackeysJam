using Assets._game.Sound.EnumInterface;
using Assets._game.Sound.SO;
using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Assets._game.Sound.Controller {
    public class SFXService : ISFXService {

        private readonly SFXConfigSO config;

        public SFXService( SFXConfigSO config ) {
            this.config = config;
        }

        public void Play( SFXType type ) {
            EventReference reference = type switch
        {
            SFXType.NPCDrink => config.npcDrink,
            SFXType.BartenderPourBeer => config.bartenderPourBeer,
            SFXType.CashIn => config.cashIn,
            SFXType.NPCSpeech => config.npcSpeech,
            _ => default
        };

            RuntimeManager.PlayOneShot(reference);
        }
    }
}