using Assets._game.Bar.Model;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Npc.View;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.ConcreateClass {
    public class NPCConsumeOrder : INPCState {


        readonly NPCScript NPCScript;
        AlcoholOrder alcoholOrder;

        AlcoholCatalog alcoholCatalog;

        Action onComplete;

        public NPCConsumeOrder( NPCScript NPCScript ) {
            this.NPCScript = NPCScript;
        }

        public void ChangeAlcoholSO( Order order ) {
            alcoholOrder = (AlcoholOrder)order;
        }

        void INPCState.EnterState( Action onComplete ) {
            this.onComplete = onComplete;

            if ( alcoholCatalog == null ) alcoholCatalog = NPCScript.alcoholCatalog;


            if ( alcoholOrder == null ) {
                Debug.Log("alcohol order is null");
                return;
            }

            var so = alcoholCatalog.Get(alcoholOrder.alcoholType);

            if ( so == null ) {
                Debug.Log("so is null");
                return;
            }

            Debug.Log($"Start consume {so.AlcoholType}, start waiting for {so.ConsumeTime}");

            NPCScript.WaitForConsumeOrder(so.ConsumeTime, () => {
                NPCScript.Leave();
            });


        }

        void INPCState.ExitState() {
            Debug.Log("Finish consume, start to go out");
        }


        void INPCState.UpdateState() {

        }



    }
}