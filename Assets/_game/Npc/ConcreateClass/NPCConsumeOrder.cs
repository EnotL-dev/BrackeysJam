using Assets._game.Bar.Model;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._game.Npc.ConcreateClass {
    public class NPCConsumeOrder : INPCState {


        readonly NPCScript NPCScript;

        OrderType orderType;

        

        public NPCConsumeOrder(NPCScript NPCScript ) {
            this.NPCScript = NPCScript;
        }

        void INPCState.EnterState() {

            Debug.Log($"Start consume {orderType}");

            NPCScript.WaitForConsumeOrder(5f, () => //hard code for now
            {
                NPCScript.Leave();
            });


        }

        void INPCState.ExitState() {
            Debug.Log("Finish consume");
        }

        
        void INPCState.UpdateState() {
            throw new System.NotImplementedException();
        }



    }
}