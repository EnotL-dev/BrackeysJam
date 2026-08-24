using Assets._game.Interaction.View;
using Assets._game.Player.View;
using System.Collections;
using UnityEngine;

namespace Assets._game.Player.Controller
{
    public interface IPlayerInteractionService
    {
        bool IsBusy();
        void Init(PlayerController playerController);
        void StartInteraction(IInteractable interactableObject);
        void ContinuousInteraction();
        void EndInteraction();
    }
}