using Assets._game.Interaction.View;
using UnityEngine;

namespace Assets._game.Player.Controller
{
    public class PlayerInteractionService : IPlayerInteractionService
    {
        private bool Busy = false;
        public bool IsBusy() => Busy;

        private PlayerController playerController;
        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
            Debug.Log("PlayerInteractionService was init");
        }

        private IInteractable lastInteractableObject;
        public void StartInteraction(IInteractable interactableObject)
        {
            Busy = true;

            Debug.Log("interact");

            if ( interactableObject.FreezePlayer() ) {
                //playerController?.FreezeMovement();
                playerController?.SetInputEnabled(false);
            }

            //interactableObject?.OnStartInteraction();
            lastInteractableObject = interactableObject;
            interactableObject?.OnInteract();

        }

        public void ContinuousInteraction()
        {
            lastInteractableObject?.OnContinuousInteraction();
        }

        public void EndInteraction()
        {
            if ( lastInteractableObject.FreezePlayer() ) {
                //playerController.UnFreezeMovement();
                playerController?.SetInputEnabled(true);
            }

            lastInteractableObject?.OnEndInteraction();

            Busy = false;
        }
    }
}