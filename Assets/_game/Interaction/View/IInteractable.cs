using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View
{
    public interface IInteractable
    {
        bool FreezePlayer();
        void OnStartInteraction();
        void OnContinuousInteraction();
        void OnEndInteraction();
    }
}