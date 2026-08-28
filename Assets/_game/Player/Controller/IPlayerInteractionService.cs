using Assets._game.Interaction.View;
using Assets._game.Player.View;
using System.Collections;
using UnityEngine;
using Zenject.SpaceFighter;

namespace Assets._game.Player.Controller
{
    public interface IPlayerInteractionService
    {
        bool IsBusy();
        void Init(PlayerController playerController, DragManagerView dragManagerView);
        void StartInteraction(IInteractable interactableObject);
        void ContinuousInteraction();
        void EndInteraction();

        bool HasOpenUI();
        void ToggleUI( IPlayerUI ui );
        void OpenUI( IPlayerUI ui );
        void CloseCurrentUI();

    }
}