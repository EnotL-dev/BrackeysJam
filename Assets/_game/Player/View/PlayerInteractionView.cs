using Assets._game.Interaction.View;
using Assets._game.Player.Controller;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets._game.Player.View
{
    public class PlayerInteractionView : MonoBehaviour
    {
        [Inject] IPlayerInteractionService interactionService;

        [SerializeField] private InputActionReference interactAction;
        [Space(5)]
        [SerializeField] private UIInteractionView uiInteractionView;
        [SerializeField] private Camera cam;
        [SerializeField] private float distanceToInteract = 3f;
        [SerializeField] private LayerMask interactLayer;

        private void Start()
        {
            interactAction.action.Enable();
        }

        private void OnDisable()
        {
            interactAction.action.Disable();
        }

        private void Update()
        {
            CheckInteraction();
        }

        private void CheckInteraction()
        {
            if (interactAction.action.WasPressedThisFrame())
            {
               if(CheckObject() is InteractableObjectView interactableObjectView)
               {
                    interactionService.InitInteraction(interactableObjectView);
                    uiInteractionView.HideTip();
               }
            }
            else
            {
                if (CheckObject() is InteractableObjectView interactableObjectView)
                {
                    uiInteractionView.ShowTip();
                }
                else
                {
                    uiInteractionView.HideTip();
                }
            }
        }

        private InteractableObjectView CheckObject()
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            Ray ray = cam.ScreenPointToRay(screenCenter);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanceToInteract, interactLayer))
            {
                return hit.collider.gameObject.GetComponent<InteractableObjectView>();
            }

            return null;
        }
    }
}