using Assets._game.Interaction.View;
using Assets._game.Player.Controller;
using Assets._game.Store.Model;
using Assets._game.UI.View;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets._game.Player.View {
    public class PlayerInteractionView : MonoBehaviour {
        [Inject] CameraShakingView cameraShakingView;
        [Inject] ArmsAnimatorView armsAnimatorView;
        [Inject] IPlayerInteractionService interactionService;

        [SerializeField] private InputActionReference attackAction;
        [SerializeField] private InputActionReference interactClose;
        [SerializeField] private InputActionReference interactAction;

        [Space(5)]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private DragManagerView dragManagerView;

        [Space(5)]
        [SerializeField] private UIInteractionView uiInteractionView;
        [SerializeField] private SettingPanel settingPanel;

        [Space(5)]
        [SerializeField] private Camera cam;
        [SerializeField] private float distanceToInteract = 3f;
        [SerializeField] private LayerMask interactLayer;

        [SerializeField] private float attackInputDelay = 0.1f;

        private float attackBlockedUntil;

        private bool holdStart = false;
        IInteractable lastInteractable = null;
        IInteractable hoveredInteraction = null;
        IPlayerUI currentUI;

        private void Start() {
            interactionService.Init(playerController, dragManagerView);
            attackAction.action.Enable();
            interactAction.action.Enable();
            interactClose.action.Enable();

            interactClose.action.performed += OnEscape;
        }

        private void OnEnable() {
            attackAction.action.performed += TryAttack;
        }


        private void OnDisable() {
            interactClose.action.performed -= OnEscape;
            attackAction.action.performed -= TryAttack
;
            attackAction.action.Disable();
            interactAction.action.Disable();
            interactClose.action.Enable();


        }

        private void Update() {
            IInteractable interactable = CheckObject();

            bool hasTarget = interactable != null;
            bool canInteract = hasTarget && interactable.CanInteractThisFrame();


            if ( canInteract ) {
                //Debug.Log($"canInteract {canInteract} this frame");
                UpdateInteractionUI(interactable);
            }
            else {
                uiInteractionView.HideTip();
                if ( hoveredInteraction != null )
                    hoveredInteraction.HideOutline();
            }

            if ( canInteract ) CheckInteraction(interactable);

            CheckInteractionRelease();
        }

        private void LateUpdate() {
            armsAnimatorView.ChangeAnimation("Punch", false);
        }

        private void UpdateInteractionUI( IInteractable interactable ) {
            if ( interactable != null && lastInteractable == null ) {
                uiInteractionView.ShowTip(interactable.GetTip());
                interactable.ShowOutline();
                hoveredInteraction = interactable;
            }
            else {
                uiInteractionView.HideTip();
                if ( hoveredInteraction != null ) {
                    hoveredInteraction.HideOutline();
                    hoveredInteraction = null;
                }
            }
        }


        private void CheckInteraction( IInteractable interactable ) {
            if ( interactable == null ) return;

            if ( interactAction.action.WasPressedThisFrame() ) {
                IFurniture furniture = interactable as IFurniture;
                if ( interactionService.IsBusy() )
                    return;
                if ( furniture != null )
                    if ( !furniture.CanBuy() && furniture.WasRemoved == false )
                        return;

                holdStart = true;

                attackBlockedUntil = Time.time + attackBlockedUntil;

                interactionService?.StartInteraction(interactable);
                uiInteractionView.HideTip();
                if ( hoveredInteraction != null )
                    hoveredInteraction.HideOutline();

                lastInteractable = interactable;

                if ( interactable.IsDraggableObject() )
                    armsAnimatorView.ChangeAnimation("Hold", true);
            }
            else if ( interactAction.action.IsPressed() ) {
                if ( interactable.IsDraggableObject() ) {
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

            attackBlockedUntil = Time.time + attackInputDelay;

            armsAnimatorView.ChangeAnimation("Hold", false);
        }

        public void ForcedInteractionRelease() // May use from any space if use container
        {
            holdStart = false;
            interactionService?.EndInteraction();
            lastInteractable = null;

            attackBlockedUntil = Time.time + attackInputDelay;

            armsAnimatorView.ChangeAnimation("Hold", false);
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

        private void OnEscape( InputAction.CallbackContext context ) {
            // If currently interacting with an object,
            // stop that interaction first.
            if ( interactionService.IsBusy() ) {
                ForcedInteractionRelease();
                return;
            }

            if ( interactionService.HasOpenUI() ) {
                interactionService.CloseCurrentUI();
                return;
            }

            // If settings is already open, close it.
            if ( settingPanel != null && settingPanel.IsOpen ) {
                settingPanel.Close();

                playerController.SetInputEnabled(true);
                playerController.SetMouseFocus(true);

                return;
            }

            // Otherwise open settings.
            OpenSettings();
        }

        private void OpenSettings() {
            if ( settingPanel == null )
                return;

            uiInteractionView.HideTip();
            if ( hoveredInteraction != null )
                hoveredInteraction.HideOutline();

            settingPanel.Open();

            playerController.SetInputEnabled(false);
            playerController.SetMouseFocus(false);
        }

        private void TryAttack( InputAction.CallbackContext _ ) {

            if ( Time.time < attackBlockedUntil ) return;

            IInteractable interactable = CheckObject();

            if ( interactable == null ) return;
            if ( interactable.IsDameableObject() ) {
                interactable.TryAttack();

                cameraShakingView.ShakePunch();
                armsAnimatorView.ChangeAnimation("Punch", true);
            }
        }
    }
}