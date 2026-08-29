using Assets._game.Interaction.View;
using Assets._game.Player.View;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Assets._game.Player.Controller {
    public class PlayerInteractionService : IPlayerInteractionService {
        private bool Busy = false;

        private IInteractable lastInteractableObject;
        private IPlayerUI currentUI;


        private PlayerController playerController;
        private DragManagerView dragManagerView;

        public bool IsBusy() => Busy;

        public void Init( PlayerController playerController, DragManagerView dragManagerView ) {
            this.playerController = playerController;
            this.dragManagerView = dragManagerView;
            Debug.Log("PlayerInteractionService was init");
        }


        public void StartInteraction( IInteractable interactableObject ) {
            Busy = true;

            lastInteractableObject = interactableObject;
            interactableObject?.OnInteract();

            if ( interactableObject.FreezePlayer() ) {
                playerController?.SetInputEnabled(false);
            }

            if ( lastInteractableObject.IsDraggableObject() )
                dragManagerView.Grab(lastInteractableObject);

            if ( lastInteractableObject.ShowCursor() )
                playerController.SetMouseFocus(false);
        }

        public void ContinuousInteraction() {
            lastInteractableObject?.OnContinuousInteraction();
        }

        public void EndInteraction() {
            if ( lastInteractableObject == null ) return;

            if ( lastInteractableObject.FreezePlayer() ) {
                //playerController.UnFreezeMovement();
                playerController?.SetInputEnabled(true);
            }

            lastInteractableObject?.OnEndInteraction();

            if ( lastInteractableObject.IsDraggableObject() )
                dragManagerView.Drop();

            if ( lastInteractableObject.ShowCursor() )
                playerController.SetMouseFocus(true);

            Busy = false;
        }

        #region  Depreticated_UI

        public bool HasOpenUI() {
            return currentUI != null && currentUI.IsOpen;
        }

        public void ToggleUI( IPlayerUI ui ) {
            // Something is already open
            if ( HasOpenUI() ) {
                CloseCurrentUI();
                return;
            }

            OpenUI(ui);
        }



        public void OpenUI( IPlayerUI ui ) {
            if ( ui == null )
                return;

            currentUI = ui;

            playerController?.SetInputEnabled(false);

            ui.Open();
        }

        public void CloseCurrentUI() {
            playerController?.SetInputEnabled(true);
            playerController?.SetMouseFocus(true);

            //THIS Have to effect for now
            if ( !HasOpenUI() ) {
                currentUI = null;
                return;
            }

            currentUI.Close();

            currentUI = null;

        }

        #endregion
    }
}