using Assets._game.Npc.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets._game.TestingScript {
    public class WaitingLineService {

        private readonly WaitingLineScript waitingLine;

        private readonly List<NPCScript> queue = new();

        public event Action<int> OnQueueChanged;

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

            int index = queue.IndexOf(npc);
            if ( index < 0 ) {
                // NPC entered the collider without a valid reservation
                return false;
            }

            Debug.Log($"[{waitingLine.name}] {npc.name} entered trigger at queue index {index}");
            return true;
        }


        public bool TryReserve( NPCScript npc, out Vector3 targetPosition ) {
            targetPosition = Vector3.zero;

            if ( npc == null || queue.Contains(npc) || queue.Count >= waitingLine.MaxCap )
                return false;

            queue.Add(npc);
            int index = queue.Count - 1;
            targetPosition = waitingLine.GetPosition(index);

            UpdateOccupied(queue.Count);
            return true;
        }

        public void Exit( NPCScript npc ) {
            if ( npc == null ) return;
            int index = queue.IndexOf(npc);
            if ( index < 0 ) return;

            queue.RemoveAt(index);
            Reorganize();
        }

        private void Reorganize() {
            for ( int i = 0; i < queue.Count; i++ ) {
                queue[i].ReOrganizeInLine(waitingLine.GetPosition(i));
            }
            UpdateOccupied(queue.Count);
        }

        public void CancelReservation( NPCScript npc ) {
            if ( npc == null ) return;

            int index = queue.IndexOf(npc);

            if ( index < 0 ) return;

            queue.RemoveAt(index);

            Reorganize();
        }

        private void UpdateOccupied( int amt ) {
            waitingLine.UpdateOccupied(amt);
        }

    }
}