using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets._game.Player.View
{
    public class ArmsAnimatorView : MonoBehaviour
    {
        [SerializeField] private Animator armsAnimator;

        public void ChangeAnimation(string namePos, bool value)
        {
            armsAnimator.SetBool(namePos, value);
        }
    }
}