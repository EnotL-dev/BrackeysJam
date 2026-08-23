using System.Collections;
using UnityEngine;

namespace Assets._game.Interaction.View
{
    public class InteractableObjectView : MonoBehaviour, IInteractable
    {

        [SerializeField] private bool freezePlayer = true;
        public bool FreezePlayer() => freezePlayer;
    }
}