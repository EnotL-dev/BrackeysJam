using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._game.Npc.Animation {
    public class NPCAnimationControllerTest : MonoBehaviour {

        [Header("References")]
        [SerializeField] private Animator animator;

        [Header("Test Movement")]
        [SerializeField, Range(0f, 5f)]
        private float speed = 0f;

        [Header("Test Action")]
        [SerializeField]
        private NPCActionState actionState = NPCActionState.None;

        [SerializeField]
        private NPCLocomotionState locomotionLayer;

        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
        private static readonly int IsSittingHash = Animator.StringToHash("IsSitting");

        private NPCAnimationController animationController;

        private static readonly int SpeedHash = Animator.StringToHash("speed");
        private static readonly int SitDownHash = Animator.StringToHash("SitDown");
        private static readonly int StandUpHash = Animator.StringToHash("StandUp");


        private static readonly int LocomotionState = Animator.StringToHash("LocomotionLayer");

        private static readonly int ActionState = Animator.StringToHash("ActionLayer");

        private void Update() {
            UpdateLocomotion(speed);
            SetAction(actionState);
        }

        public void UpdateLocomotion( float speed ) {
            bool moving = speed > 0.01f;

            SetLocomotion(moving);
        }

        public void SetLocomotion( bool move ) {
            animator.SetBool(IsMovingHash, move);
        }

        public void SetAction( NPCActionState state ) {
            if ( state == NPCActionState.Sit ) {
                animator.SetBool(IsSittingHash, true);
            }
            else if ( state == NPCActionState.StandUp ) {
                animator.SetBool(IsSittingHash, false);
            }

        }
    }
}