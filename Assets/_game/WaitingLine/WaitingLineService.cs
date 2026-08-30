using Assets._game.Npc.View;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets._game.TestingScript {
    public class WaitingLineService {

        private readonly WaitingLineScript waitingLine;

        private readonly Dictionary<int, NPCScript> queue = new();


        public WaitingLineService( WaitingLineScript waitingLine ) {
            this.waitingLine = waitingLine;

            SetUpObserver();
        }

        void SetUpObserver() {
            if ( waitingLine == null ) return;
            waitingLine.OnNpcTriggerEnter += HandleNpcTriggerEnter;
            waitingLine.OnNpcTriggerExit += HandleNpcTriggerExit;
        }

        public void Dispose() {
            if ( waitingLine == null ) return;
            waitingLine.OnNpcTriggerEnter -= HandleNpcTriggerEnter;
            waitingLine.OnNpcTriggerExit -= HandleNpcTriggerExit;
        }


        void HandleNpcTriggerEnter( NPCScript npc ) => Enter(npc);


        void HandleNpcTriggerExit( NPCScript npc ) => Exit(npc);


        public bool Enter( NPCScript npc ) {
            if ( npc == null ) return false;

            int index = FindNpcIndex(npc);

            if ( index < 0 ) {
                // NPC is just passing through the trigger.
                return false;
            }

            Debug.Log($"[{waitingLine.name}] {npc.name} entered queue at index {index}");

            return true;
        }

        public void Exit( NPCScript npc ) {
            if ( npc == null ) return;

            int index = FindNpcIndex(npc);

            if ( index < 0 ) return;

            queue.Remove(index);

            Reorganize();
        }

        private void Reorganize() {
            if ( queue.Count == 0 ) return;

            Debug.Log($"current queue have {queue.Count}");

            List<NPCScript> orderedNpcs = queue
                    .OrderBy(pair => pair.Key)
                    .Select(pair => pair.Value)
                    .ToList();

            queue.Clear();

            for ( int i = 0; i < orderedNpcs.Count; i++ ) {
                NPCScript npc = orderedNpcs[i];

                queue[i] = npc;

                npc.MoveToDest(waitingLine.GetPosition(i));
            }

        }

        public bool TryReserve( NPCScript npc, out Vector3 targetPosition ) {
            targetPosition = Vector3.zero;

            if ( npc == null ) return false;

            if ( queue.ContainsValue(npc) )
                return false;

            int index = GetNextAvailableIndex();

            if ( index >= waitingLine.MaxCap )
                return false;

            queue[index] = npc;

            targetPosition = waitingLine.GetPosition(index);

            return true;
        }

        public void CancelReservation( NPCScript npc ) {
            if ( npc == null ) return;

            int index = FindNpcIndex(npc);

            if ( index < 0 ) return;

            queue.Remove(index);

            //foreach ( var kvp in queue ) {
            //    Debug.Log($"[QUEUE] CANCEL {npc.name} | " +
            //                $"index={index} | " +
            //                $"contains={queue.ContainsValue(npc)}");
            //}

            //Reorganize();
        }

        private int GetNextAvailableIndex() {
            for ( int i = 0; i < waitingLine.MaxCap; i++ ) {
                if ( !queue.ContainsKey(i) )
                    return i;
            }

            return waitingLine.MaxCap;
        }

        private int FindNpcIndex( NPCScript npc ) {
            foreach ( var pair in queue ) {
                if ( pair.Value == npc ) return pair.Key;
            }

            return -1;
        }
    }
}