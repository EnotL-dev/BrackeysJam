using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Bar.Model.SOScript.FoodSO;
using Assets._game.Npc;
using Assets._game.TestingScript;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller {
    public class BarService : IBarService, IInitializable {

        WaitingLineService waitingLineService;


        SeatService seatService;


        [Inject]
        void Construct( [Inject(Id = "Bar")] WaitingLineService waitingLine
            , SeatService seatService ) {
            this.waitingLineService = waitingLine;
            this.seatService = seatService;
        }

        //private List<AlchoholDictionary> alchoholDictionary;
        AlcoholSO[] alcohols;


        public void Initialize() {
            InitAlchoholData();
        }

        private void InitAlchoholData() {

            alcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol");

            foreach ( AlcoholSO alcohol in alcohols ) {
                Debug.Log($"{alcohol.Type} - {alcohol.BuyCost}");
            }

            //Debug.Log(alchoholDictionary.Count);
        }
        

        
        public IEnumerator RequestOrder( NPCScript NPCScript, Order order, Action onOrderReady ) {
            switch ( order ) {
                case FoodOrder:
                    yield return MakeFood(NPCScript, order);
                    break;

                case DrinkOrder:
                    yield return MakeDrink(NPCScript, order);
                    break;

                default:
                    yield return MakeFood(NPCScript, null);
                    break;
            }

            onOrderReady?.Invoke();
        }






        //TOOD: it shuold read the SO instead of hardcode
        public IEnumerator MakeFood( NPCScript NPCScript, Order order ) {

            yield return new WaitForSeconds(10);

            Debug.Log("food ready");


        }

        public IEnumerator MakeDrink( NPCScript NPCScript, Order order ) {
            yield return new WaitForSeconds(5);

            Debug.Log("drink ready");


        }
    }
}
