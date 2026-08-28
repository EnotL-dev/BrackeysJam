using Assets._game.Player.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class ButtonExitInteractionView : MonoBehaviour
    {
        [Inject] PlayerInteractionView playerInteractionView;

        public void ForcedInteractionRelease()
        {
            playerInteractionView.ForcedInteractionRelease();
        }
    }
}