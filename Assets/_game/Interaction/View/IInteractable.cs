using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View
{
    public interface IInteractable
    {
        string GetTip() => "E";

        void OnInteract();

        bool FreezePlayer();
        bool IsDragingObject();
        void OnStartInteraction();
        void OnContinuousInteraction();
        void OnEndInteraction();
    }
}