using Assets._game.Store.Model;
using Assets._game.Store.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableCannonView : MonoBehaviour, IInteractable
    {
        public string GetTip()
        {
            return "[E] - SHOOT!";
        }

        public bool CanShoot = false;

        public bool FreezePlayer() => false;
        public bool IsDraggableObject() => false;
        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {

        }

        public void OnInteract()
        {
            if(CanShoot)
            {
                CanShoot = false;
                Debug.Log("Shooted");
            }
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}