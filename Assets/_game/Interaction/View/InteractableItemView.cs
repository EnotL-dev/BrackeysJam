using Assets._game.Store.Model;
using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View
{
    public class InteractableItemView : MonoBehaviour, IInteractable, IFurniture
    {
        public bool FreezePlayer() => false;
        public bool IsDraggableObject() => true;
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