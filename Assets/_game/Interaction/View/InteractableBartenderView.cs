using Assets._game.Bar.Controller;
using Assets._game.UI.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableBartenderView : MonoBehaviour, IInteractable
    {
        // [Inject] private readonly IBarService barService;
        // [Inject] private readonly IEconomyService economyService;

        [SerializeField] private BartenderPanelView bartenderPanelView;

        public bool FreezePlayer() => true;

        public bool IsDraggableObject() => false;

        public bool ShowCursor() => true;

        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            bartenderPanelView.ClosePanel();
        }

        public void OnInteract()
        {
            bartenderPanelView.OpenPanel();
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}