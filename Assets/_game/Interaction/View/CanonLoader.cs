using Assets._game.Player.Controller;
using Assets._game.Player.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class CanonLoader : MonoBehaviour
    {
        [Inject] PlayerInteractionView playerInteractionView;

        [SerializeField] private InteractableCannonView interactableCannonView;
        [SerializeField] private LayerMask layerMaskVisitor;

        private bool IsBusy = false;
        public void UnLoadCanon()
        {
            IsBusy = false;
            playerInteractionView.ForcedInteractionRelease();
            interactableCannonView.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(((1 << other.gameObject.layer) & layerMaskVisitor) != 0 && other.CompareTag("NPC") && !IsBusy)
            {
                playerInteractionView.ForcedInteractionRelease();
                interactableCannonView.gameObject.SetActive(true);
                interactableCannonView.LoadVisitor(other.transform);

                IsBusy = true;
            }
        }
    }
}