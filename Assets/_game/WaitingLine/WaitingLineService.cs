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
        }

        public bool HasAvailableSlot() {
            return waitingLine.HasAvailableSlot();
        }

        public bool Enter( NPCScript npc ) {
            if ( npc == null ) return false;

            if ( !HasAvailableSlot() ) return false;

            if ( npcs.Contains(npc) ) return false;

            npcs.Add(npc);

            npc.MoveToDest(
                waitingLine.GetPosition(npcs.Count - 1)
            );

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