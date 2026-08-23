using Assets._game.Interaction.View;
using System.Collections;
using UnityEngine;

namespace Assets._game.Player.Controller
{
    public interface IPlayerInteractionService
    {
        void InitInteraction(IInteractable interactableObject);
    }
}