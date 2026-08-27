using Assets._game.Interaction.View;
using Assets._game.Player.Controller;
using Assets._game.Store.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Zenject;

namespace Assets._game.Player.View {
    public class PlayerInteractionView : MonoBehaviour {
        [Inject] IPlayerInteractionService interactionService;

        [SerializeField] private InputActionReference interactClose;
        [SerializeField] private InputActionReference interactAction;
        [Space(5)]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private DragManagerView dragManagerView;
        [Space(5)]
        [SerializeField] private UIInteractionView uiInteractionView;
        [SerializeField] private Camera cam;
        [SerializeField] private float distanceToInteract = 3f;
        [SerializeField] private LayerMask interactLayer;

        private bool holdStart = false;

        private void Start() {
            interactionService.Init(playerController, dragManagerView);
            interactAction.action.Enable();
        }

        private void OnDisable() {
            interactAction.action.Disable();
        }

        private void Update() {
            IInteractable interactable = CheckObject();

            bool hasTarget = interactable != null;
            bool canInteract = hasTarget && interactable.CanInteractThisFrame;

            if ( canInteract ) UpdateInteractionUI(interactable);
            else uiInteractionView.HideTip();

            if ( canInteract ) CheckInteraction(interactable);

            CheckInteractionRelease();
        }

        private void UpdateInteractionUI( IInteractable interactable ) {
            if ( interactable != null && lastInteractable == null) {
                uiInteractionView.ShowTip(interactable.GetTip());
            }
            else {
                uiInteractionView.HideTip();
            }
        }

        IInteractable lastInteractable = null;
        private void CheckInteraction( IInteractable interactable ) {
            if ( interactable == null ) return;

            if ( interactAction.action.WasPressedThisFrame() ) {
                IFurniture furniture = interactable as IFurniture;
                if ( interactionService.IsBusy() || (furniture != null && !furniture.CanBuy()))
                    return;

                holdStart = true;

                interactionService.StartInteraction(interactable);
                uiInteractionView.HideTip();

                lastInteractable = interactable;
            }
            else if ( interactAction.action.IsPressed() ) {
                if (interactable.IsDraggableObject())
                {
                    interactionService.ContinuousInteraction();
                }
            }
        }

        private void CheckInteractionRelease() {
            if ( !holdStart || lastInteractable == null ) return;

            if ( interactClose.action.WasReleasedThisFrame() && !lastInteractable.IsDraggableObject() ) {
                EndInteraction();
            }
            else if ( interactAction.action.WasReleasedThisFrame() && (lastInteractable.OnceActivation() || lastInteractable.IsDraggableObject()) ) {
                EndInteraction();
            }
        }

        private void EndInteraction() {
            holdStart = false;
            interactionService?.EndInteraction();
            lastInteractable = null;
        }

        public void ForcedInteractionRelease() // May use from any space if use container
        {
            holdStart = false;
            interactionService?.EndInteraction();
            lastInteractable = null;
        }

        private IInteractable CheckObject() {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            Ray ray = cam.ScreenPointToRay(screenCenter);

            RaycastHit hit;

            if ( Physics.Raycast(ray, out hit, distanceToInteract, interactLayer) ) {
                return hit.collider.gameObject.GetComponent<IInteractable>();
            }

            return null;
        }
    }
}