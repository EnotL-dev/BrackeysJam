using Assets._game.Interaction.View;
using Assets._game.Player.View;
using UnityEngine;

namespace Assets._game.Player.Controller
{
    public class PlayerInteractionService : IPlayerInteractionService
    {
        private bool Busy = false;
        public bool IsBusy() => Busy;

        private PlayerController playerController;
        private DragManagerView dragManagerView;
        public void Init(PlayerController playerController, DragManagerView dragManagerView)
        {
            this.playerController = playerController;
            this.dragManagerView = dragManagerView;
            Debug.Log("PlayerInteractionService was init");
        }

        private IInteractable lastInteractableObject;
        public void StartInteraction(IInteractable interactableObject)
        {
            Busy = true;

            Debug.Log("interact");

            if ( interactableObject.FreezePlayer() ) {
                playerController?.SetInputEnabled(false);
            }

            lastInteractableObject = interactableObject;
            interactableObject?.OnInteract();

            if (lastInteractableObject.IsDragingObject())
                dragManagerView.Grab(lastInteractableObject);

            if (lastInteractableObject.ShowCursor())
                playerController.SetMouseFocus(true);
        }

        public void ContinuousInteraction()
        {
            lastInteractableObject?.OnContinuousInteraction();
        }

        public void EndInteraction()
        {
            if (lastInteractableObject == null) return;

            if ( lastInteractableObject.FreezePlayer() ) {
                //playerController.UnFreezeMovement();
                playerController?.SetInputEnabled(true);
            }

            lastInteractableObject?.OnEndInteraction();

            if (lastInteractableObject.IsDragingObject())
                dragManagerView.Drop();

            if (lastInteractableObject.ShowCursor())
                playerController.SetMouseFocus(false);

            Busy = false;
        }
    }
}