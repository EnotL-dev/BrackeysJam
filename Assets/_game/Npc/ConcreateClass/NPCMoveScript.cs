using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

namespace Assets._game.Npc.ConcreateClass {
    public class NPCMoveScript : INPCState {

        readonly NPCScript nPCScript;


        Transform transform;
        NavMeshAgent agent;

        private Action onComplete;
        private float ArrivalThreshold = 0.1f;

        float timeMoving = 5;

        Vector3 dest;


        public NPCMoveScript( NPCScript nPCScript ) {
            this.nPCScript = nPCScript;
        }

        public void SetDestination( Vector3 pos ) {
            dest = pos;
        }



        public void EnterState( Action onComplete ) {
            //use ai navigation for this

            //For testing

            //Debug.Log($"move this npc to some {dest}");

            if(agent == null ) {
                agent = nPCScript.agent;
            }

            agent.SetDestination(dest);

            this.onComplete = onComplete;
        }

        public void ExitState() {
            //throw new System.NotImplementedException();

            //Debug.Log("exit move");
            onComplete = null;
        }

        public void UpdateState() {
            if ( onComplete == null ) return;

            if ( agent.pathPending ) return;

            if ( agent.remainingDistance > agent.stoppingDistance + ArrivalThreshold )
                return;

            if ( agent.hasPath && agent.velocity.sqrMagnitude > 0.01f )
                return;

            Action callback = onComplete; //prevent update multiple time
            onComplete = null;

            callback?.Invoke();
        }

    }
}