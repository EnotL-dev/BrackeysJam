using Assets._game.Bar.Controller;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableIBartenderView : MonoBehaviour, IInteractable
    {
        [Inject] private readonly IBarService barService;
        [Inject] private readonly IEconomyService economyService;

        public bool FreezePlayer() => true;

        public bool IsDragingObject() => false;

        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            
        }

        public void OnInteract()
        {
            
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}