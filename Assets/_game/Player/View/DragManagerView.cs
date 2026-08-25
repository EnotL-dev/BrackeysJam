using Assets._game.Interaction.View;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._game.Player.View
{
    public class DragManagerView : MonoBehaviour
    {
        [SerializeField] private float dragSpeed = 5f;
        [SerializeField] private float maxDragVelocity = 15f;
        [SerializeField] private float dragDamping = 10f;
        private float originalDamping;
        [Space(5)]
        [SerializeField] private Camera cam;
        private Rigidbody draggedRB;
        private float dragDistance;

        public void Grab(IInteractable interactable)
        {
            if (draggedRB == null)
                draggedRB = (interactable as MonoBehaviour).gameObject.GetComponent<Rigidbody>();

            draggedRB.freezeRotation = true;
            originalDamping = draggedRB.linearDamping;
            draggedRB.linearDamping = dragDamping;

            dragDistance = Vector3.Distance(draggedRB.position, cam.transform.position);
        }

        public void Drop()
        {
            if (draggedRB != null)
            {
                draggedRB.freezeRotation = false;

                draggedRB.linearDamping = originalDamping;
                draggedRB.linearVelocity = Vector3.ClampMagnitude(draggedRB.linearVelocity, maxDragVelocity * 0.5f);

                draggedRB = null;
            }
        }

        private void FixedUpdate()
        {
            if (draggedRB != null)
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                Ray ray = cam.ScreenPointToRay(screenCenter);
                Vector3 targetPos = ray.GetPoint(dragDistance);

                Vector3 direction = targetPos - draggedRB.transform.position;

                Vector3 targetVelocity = direction * dragSpeed;
                draggedRB.linearVelocity = Vector3.ClampMagnitude(targetVelocity, maxDragVelocity);
            }
        }
    }
}