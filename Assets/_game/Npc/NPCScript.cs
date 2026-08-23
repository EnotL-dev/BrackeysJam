using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCScript : MonoBehaviour {

        readonly NPCMachineState machineState = new NPCMachineState(); //might use DI
        
        public NPCMoveScript moveScript;

        public NpcWaitingScript waitScript;

        void Start() {
            moveScript = new NPCMoveScript(this.gameObject.transform, machineState);
            waitScript = new NpcWaitingScript(machineState);

            machineState.Initialize(moveScript);
        }
    }
}