using Assets._game.Npc.View;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._game.Npc.Animation {
    public class NPCAnimationController {

        private readonly NPCScript npcScript;
        private Animator animator;
        private NavMeshAgent agent;

        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int IsSittingHash = Animator.StringToHash("IsSitting");
        private static readonly int Drink = Animator.StringToHash("Drink");

        public NPCAnimationController( NPCScript script ) {
            this.npcScript = script;
        }

        public void UpdateAnimation() {
            UpdateLocomotion();
        }


        void UpdateLocomotion() {
            agent ??= npcScript.agent;
            animator ??= npcScript.animator;

            //if ( animator == null ) Debug.Log("error can't find animator");

            bool moving = agent.velocity.sqrMagnitude > 0.01f;

            SetLocomotion(moving);
        }


        void SetLocomotion( bool move ) {
            animator.SetBool(IsMovingHash, move);
        }


        public void SetAction( NPCActionState state ) {
            Debug.Log($" SetAction: {state}");

            if ( state == NPCActionState.Sit ) {
                animator.SetBool(IsSittingHash, true);
            }
            else if ( state == NPCActionState.StandUp ) {
                animator.SetBool(IsSittingHash, false);
            }
            else if ( state == NPCActionState.ConsumeOrder ) {
                animator.SetTrigger(Drink);
            }

        }

    }



}