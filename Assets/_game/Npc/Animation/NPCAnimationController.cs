using System.Collections;
using UnityEngine;

namespace Assets._game.Npc.Animation {
    public class NPCAnimationController {

        private readonly Animator animator;

        private static readonly int LocomotionState =
        Animator.StringToHash("LocomotionState");

        private static readonly int ActionState =
        Animator.StringToHash("ActionState");

        public NPCAnimationController( Animator animator ) {
            this.animator = animator;
        }

        public void SetLocomotion( NPCActionState state ) {
            animator.SetInteger(LocomotionState, (int)state);
        }

        public void SetAction( NPCActionState state ) {
            animator.SetInteger(ActionState, (int)state);
        }




    }
}