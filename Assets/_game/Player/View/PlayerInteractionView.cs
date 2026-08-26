using Assets._game.Interaction.View;
using Assets._game.Player.Controller;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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

        private float timerHold = 0;
        private void Update() {
            //CheckInteraction();

            //if(holdStart && interactAction.action.WasReleasedThisFrame())
            //{
            //    holdStart = false;

            //    interactionService.EndInteraction();
            //}

            IInteractable interactable = CheckObject();

            UpdateInteractionUI(interactable);
            CheckInteraction(interactable);
            CheckInteractionRelease();

            if(timerHold > 0)
            {
                timerHold -= Time.deltaTime;
            }
        }

        //TODO: seperate code for ui and checking
        private void CheckInteraction() {
            if ( CheckObject() is IInteractable interactableObject ) {
                if ( interactAction.action.WasPressedThisFrame() ) // one click
                {
                    if ( interactionService.IsBusy() ) return;

                    holdStart = true;

                    interactionService.StartInteraction(interactableObject);
                    uiInteractionView.HideTip();
                }
                else if ( interactAction.action.IsPressed() ) // pressed
                {
                    interactionService.ContinuousInteraction();
                }
                else {
                    uiInteractionView.ShowTip(interactableObject.GetTip());
                }
            }
            else // cross
            {
                uiInteractionView.HideTip();
            }
        }

        private void UpdateInteractionUI( IInteractable interactable ) {
            if ( interactable != null ) {
                uiInteractionView.ShowTip(interactable.GetTip());
            }
            else {
                uiInteractionView.HideTip();
            }
        }

        private void CheckInteraction( IInteractable interactable ) {
            if ( interactable == null ) return;

            if ( interactAction.action.WasPressedThisFrame() ) {
                if ( interactionService.IsBusy() ) return;

                holdStart = true;
                timerHold = 0.1f;

                interactionService.StartInteraction(interactable);
                uiInteractionView.HideTip();
            }
            else if (interactAction.action.IsPressed()) {
                if(interactable.IsDragingObject())
                    interactionService.ContinuousInteraction();
                else
                    timerHold = 0.1f;
            }
        }

        private void CheckInteractionRelease() {
            if (timerHold > 0) return;

            if ( holdStart && interactAction.action.WasReleasedThisFrame() ) {
                holdStart = false;
                interactionService?.EndInteraction();
            }
            else if (holdStart)
            {
                if(interactClose.action.WasReleasedThisFrame())
                {
                    holdStart = false;
                    interactionService?.EndInteraction();
                }
            }
        }

        public void ForcedInteractionRelease() // May use from any space if use container
        {
            holdStart = false;
            interactionService?.EndInteraction();
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