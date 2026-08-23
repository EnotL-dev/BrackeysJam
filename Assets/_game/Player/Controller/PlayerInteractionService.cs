using Assets._game.Interaction.View;
using UnityEngine;

namespace Assets._game.Player.Controller
{
    public class PlayerInteractionService : IPlayerInteractionService
    {
        public void InitInteraction(IInteractable interactableObject)
        {
            //Debug.Log($"interacted with {interactableObject.Name()}");
        }
    }
}