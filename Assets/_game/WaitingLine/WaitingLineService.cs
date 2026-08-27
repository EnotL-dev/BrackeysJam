using Assets._game.Npc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.TestingScript {
    public class WaitingLineService {

        private readonly WaitingLineScript waitingLine;

        private readonly List<NPCScript> npcs = new();

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

        public Vector3 GetNextAvailablePosition() => waitingLine.GetPosition(npcs.Count);

        public bool Enter( NPCScript npc ) {
            if ( npc == null ) return false;
            if ( !HasAvailableSlot() ) return false;
            if ( npcs.Contains(npc) ) return false;

            npcs.Add(npc);

            return true;
        }

        public void Exit( NPCScript npc ) {
            if ( !npcs.Remove(npc) ) return;
            Reorganize();
        }

        private void Reorganize() {
            for ( int i = 0; i < npcs.Count; i++ ) {
                npcs[i].MoveToDest(
                    waitingLine.GetPosition(i)
                );
            }
        }

    }
}