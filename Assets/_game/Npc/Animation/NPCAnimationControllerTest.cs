using System.Collections;
using UnityEngine;

namespace Assets._game.Npc.Animation {
    public class NPCAnimationControllerTest : MonoBehaviour {

        [Header("References")]
        [SerializeField] private Animator animator;

        [Header("Test Locomotion")]
        [SerializeField, Range(0f, 5f)] private float speed = 0f;
        [SerializeField] private NPCLocomotionState locomotionLayer = NPCLocomotionState.Idle;

        [Header("Test Action")]
        [SerializeField] private NPCActionState actionState = NPCActionState.None;

        // Parameter Hashes
        private static readonly int SpeedHash = Animator.StringToHash("speed");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int IsSittingHash = Animator.StringToHash("IsSitting");
        private static readonly int SitDownTriggerHash = Animator.StringToHash("SitDown");
        private static readonly int StandUpTriggerHash = Animator.StringToHash("StandUp");
        private static readonly int DrinkTriggerHash = Animator.StringToHash("Drink");
        private static readonly int LocomotionStateHash = Animator.StringToHash("LocomotionState");

        // Change Detection Cache
        private float lastSpeed = -1f;
        private NPCActionState lastActionState = NPCActionState.None;
        private NPCLocomotionState lastLocomotionState = (NPCLocomotionState)(-1);

        private void Awake() {
            if ( animator == null ) {
                animator = GetComponent<Animator>();
            }
        }

        private void Update() {
            if ( animator == null ) return;

            HandleLocomotion();
            HandleActionState();
        }

        private void HandleLocomotion() {
            // Continuous speed & boolean update

            if ( speed > 0.01f ) {
                animator.SetBool(IsMovingHash, true);
            }
            else {
                animator.SetBool(IsMovingHash, false);
            }

        }

        private void HandleActionState() {
            // State actions / Triggers (only runs when you change the dropdown in Inspector)

            ApplyActionState(actionState);
        }

        public void ApplyActionState( NPCActionState state ) {
            if ( animator == null ) return;

            switch ( state ) {
                case NPCActionState.None:
                    break;

                case NPCActionState.Sit:
                    animator.SetBool(IsSittingHash, true);
                    animator.SetTrigger(SitDownTriggerHash);
                    break;

                case NPCActionState.StandUp:
                    animator.SetBool(IsSittingHash, false);
                    animator.SetTrigger(StandUpTriggerHash);
                    break;

                case NPCActionState.ConsumeOrder:
                    animator.SetTrigger(DrinkTriggerHash);
                    break;
            }
        }

        // --- Inspector Testing Buttons (Right-click component header in Inspector) ---

        [ContextMenu("Trigger Sit")]
        public void TestSit() {
            actionState = NPCActionState.Sit;
            ApplyActionState(NPCActionState.Sit);
        }

        [ContextMenu("Trigger Stand Up")]
        public void TestStandUp() {
            actionState = NPCActionState.StandUp;
            ApplyActionState(NPCActionState.StandUp);
        }

        [ContextMenu("Trigger Drink")]
        public void TestDrink() {
            actionState = NPCActionState.ConsumeOrder;
            ApplyActionState(NPCActionState.ConsumeOrder);
        }
    }
}