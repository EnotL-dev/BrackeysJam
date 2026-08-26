using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._game.Npc.Animation {
    public class NPCAnimationController {

        private readonly NPCScript npcScript;
        private Animator animator;
        private NavMeshAgent agent;

        private readonly int LocomotionState = Animator.StringToHash("LocomotionState");

        private readonly int ActionState = Animator.StringToHash("ActionState");

        public NPCAnimationController( NPCScript script ) {
            this.npcScript = script;
        }

        public void UpdateLocomotion( ) {
            if ( agent == null ) agent = npcScript.agent;

            bool moving = agent.velocity.sqrMagnitude > 0.01f;

            animator.SetInteger(
                LocomotionState,
                moving
                    ? (int)NPCLocomotionState.Walk
                    : (int)NPCLocomotionState.Idle
            );
        }



        public void SetLocomotion( NPCActionState state ) {
            animator.SetInteger(LocomotionState, (int)state);
        }

        public void SetAction( NPCActionState state ) {
            animator.SetInteger(ActionState, (int)state);
        }




    }
}