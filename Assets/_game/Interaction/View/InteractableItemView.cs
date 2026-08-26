using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View
{
    public class InteractableItemView : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool freezePlayer = false;
        public bool FreezePlayer() => freezePlayer;
        [SerializeField] private bool isDraggingObject = true;
        public bool IsDraggableObject() => isDraggingObject;
        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            //nothing
        }

        public void OnInteract()
        {
            //nothing
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}