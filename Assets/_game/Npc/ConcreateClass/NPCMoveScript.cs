using Assets._game.Npc.View;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
            //Debug.Log($"[{nPCScript.name}] Move SetDestination: {pos}");
        }



        public void EnterState( Action onComplete ) {
            //Debug.Log($"[{nPCScript.name}] MOVE EnterState");

            //use ai navigation for this

            //For testing

            //Debug.Log($"move this npc to some {dest}");

            if ( agent == null ) {
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

            if ( agent.pathPending ) {
                //Debug.Log($"[NavMesh] Path is still pending calculation for '{nPCScript.name}'.");
                return;
            }

            Vector3 flatAgentPos = new Vector3(agent.transform.position.x, 0, agent.transform.position.z);
            Vector3 flatDestPos = new Vector3(dest.x, 0, dest.z);
            float flatDistance = Vector3.Distance(flatAgentPos, flatDestPos);

            bool reachedByNavMesh = !agent.hasPath || agent.remainingDistance <= (agent.stoppingDistance + ArrivalThreshold);
            bool reachedByDistance = flatDistance <= (agent.stoppingDistance + ArrivalThreshold);

            //Debug.Log($"Remaining: {agent.remainingDistance:F2}m, {flatDistance}");

            if ( reachedByNavMesh || reachedByDistance ) {
                if ( agent.velocity.sqrMagnitude <= 0.01f ) {
                    Action callback = onComplete;
                    onComplete = null;
                    callback?.Invoke();
                }
            }
        }

        public void Stop() {
            if ( agent != null && agent.isOnNavMesh ) {
                // 1. Clears the active path and sets remaining distance to 0
                agent.ResetPath();

                // 2. Kills any residual velocity immediately
                agent.velocity = Vector3.zero;

                // 3. (Optional) Halts agent processing without clearing path
                agent.isStopped = true;

                agent.updateRotation = false;
                agent.enabled = false;
            }

            // Clear the completion callback so it doesn't trigger unexpectedly
            onComplete = null;
        }


    }
}