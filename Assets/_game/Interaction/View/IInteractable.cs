using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View {
    public interface IInteractable {
        string GetTip() => "E";
        void ShowOutline();
        void HideOutline();

        void OnInteract();

        bool FreezePlayer();
        bool CanInteractThisFrame() => true;
        bool ShowCursor() => false;
        bool IsDraggableObject() => false;
        bool IsDameableObject() => false;
        void TryAttack() { }
        bool OnceActivation() => false; // For object who dosent need end interaction and exit
        void OnStartInteraction();
        void OnContinuousInteraction();
        void OnEndInteraction();
    }
}