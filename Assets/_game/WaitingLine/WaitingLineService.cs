using Assets._game.Npc.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._game.TestingScript {
    public class WaitingLineService {

        private readonly WaitingLineScript waitingLine;

        private readonly List<NPCScript> npcs = new();

        private int reserveCount = 0;

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


        public bool HasAvailableSlot() => npcs.Count < waitingLine.MaxCap;
        public int TotalClaimedSlots => npcs.Count + reserveCount;

        public Vector3 GetNextAvailablePosition() => waitingLine.GetPosition(TotalClaimedSlots);

        public bool Enter( NPCScript npc ) {
            if ( npc == null ) return false;
            if ( npcs.Contains(npc) ) return false;

            if ( reserveCount > 0 ) {
                reserveCount--;
            }
            else if ( npcs.Count >= waitingLine.MaxCap ) {
                return false; // Queue full with no reservation
            }

            npcs.Add(npc);
            return true;
        }

        public void Exit( NPCScript npc ) {
            if ( !npcs.Remove(npc) ) return;
            Reorganize();
        }

        private void Reorganize() {
            for ( int i = 0; i < npcs.Count; i++ ) {
                npcs[i].MoveToDest(waitingLine.GetPosition(i));
            }
        }

        public bool TryReserve( out Vector3 targetPosition ) {
            if ( !HasAvailableSlot() ) {
                targetPosition = Vector3.zero;
                return false;
            }

            targetPosition = waitingLine.GetPosition(TotalClaimedSlots);
            reserveCount++;
            return true;
        }

        public void CancelReservation() {
            if ( reserveCount > 0 )
                reserveCount--;
        }
    }
}