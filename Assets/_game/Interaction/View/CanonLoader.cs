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

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == layerMaskVisitor && other.gameObject.tag == "NPC")
            {
                Debug.Log("Shooted");
                /*
                playerInteractionView.ForcedInteractionRelease();
                interactableCannonView.gameObject.SetActive(true);
                interactableCannonView.CanShoot = true;
                other.gameObject.SetActive(false);
                */
            }
        }
    }
}