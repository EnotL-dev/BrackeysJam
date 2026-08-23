using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View
{
    public class InteractableObjectView : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool freezePlayer = false;
        public bool FreezePlayer() => freezePlayer;

        public void OnStartInteraction()
        {
            Debug.Log($"I LOVE CATGIRLS");
        }

        public void OnContinuousInteraction()
        {
            Debug.Log($"pik");
        }

        public void OnEndInteraction()
        {
            Debug.Log($"And foxgirls");
        }
    }
}