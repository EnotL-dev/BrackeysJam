using Assets._game.Sound.EnumInterface;
using Assets._game.Sound.SO;
using FMOD.Studio;
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
            EventReference reference = GetEventReference(type);
            if ( !reference.IsNull ) {
                RuntimeManager.PlayOneShot(reference);
            }
        }

        // Overload for 3D positional audio
        public void PlayInSpace( SFXType type, Vector3 position ) {
            EventReference reference = GetEventReference(type);
            if ( !reference.IsNull ) {
                RuntimeManager.PlayOneShot(reference, position);
            }
        }

        public EventInstance StartLoop( SFXType type, GameObject gameObject ) {
            EventReference reference = GetEventReference(type);

            if ( reference.IsNull ) return default;

            EventInstance instance = RuntimeManager.CreateInstance(reference);

            if ( gameObject != null ) {
                RuntimeManager.AttachInstanceToGameObject(instance, gameObject);
            }

            instance.start();
            return instance;
        }


        public void StopLoop( EventInstance instance ) {
            if ( !instance.isValid() ) return;

            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }


        private EventReference GetEventReference( SFXType type ) => type switch {
            SFXType.NPCDrink => config.npcDrink,
            SFXType.BartenderPourBeer => config.bartenderPourBeer,
            SFXType.CashIn => config.cashIn,
            SFXType.NPCSpeech => config.npcSpeech,
            SFXType.Hit => config.hit,
            SFXType.KnockOut => config.knockOut,
            SFXType.GrabObject => config.GrabObject,
            SFXType.PlaceChair => config.PlaceChair,
            SFXType.PlacePlant => config.PlacePot,
            SFXType.TurnSign => config.TurnSign,
            SFXType.CanonShoot => config.CanonShoot,
            SFXType.FuseFire => config.FuseFire,
            SFXType.BreakChair => config.BreakChair,
            SFXType.BuyAlcohol => config.BuyAlcohol,
            _ => default
        };

    }

}