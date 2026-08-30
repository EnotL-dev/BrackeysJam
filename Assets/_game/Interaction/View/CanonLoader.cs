using Assets._game.Player.Controller;
using Assets._game.Player.View;
using Assets._game.Sound.EnumInterface;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View {
    public class CanonLoader : MonoBehaviour {
        [Inject] PlayerInteractionView playerInteractionView;
        [Inject] ISFXService sFXService;

        [SerializeField] private InteractableCannonView interactableCannonView;
        [SerializeField] private LayerMask layerMaskVisitor;
        [Space(5)]
        [SerializeField] private ParticleSystem particleFire;
        [SerializeField] private ParticleSystem particleFuse;

        private bool IsBusy = false;
        public void UnLoadCanon() {
            playerInteractionView.ForcedInteractionRelease();
            interactableCannonView.gameObject.SetActive(false);

            particleFire.Stop();
            particleFuse.Stop();

            IsBusy = false;
        }

        private void OnTriggerEnter( Collider other ) {
            if ( ((1 << other.gameObject.layer) & layerMaskVisitor) != 0 && other.CompareTag("NPC") && !IsBusy ) {

                sFXService.Play(SFXType.FuseFire);

                playerInteractionView.ForcedInteractionRelease();
                interactableCannonView.gameObject.SetActive(true);
                interactableCannonView.LoadVisitor(other.transform);

                particleFire.Play();
                particleFuse.Play();

                IsBusy = true;
            }
        }
    }
}