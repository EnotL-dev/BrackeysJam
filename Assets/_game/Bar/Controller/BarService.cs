using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Bar.Model.SOScript.FoodSO;
using Assets._game.Npc;
using Assets._game.TestingScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private List<AlchoholDictionary> alchohols = new List<AlchoholDictionary>();

        public AlchoholDictionary GetAlcoholDictionary(AlcoholType alcoholType) => alchohols.Find(a => a.alchohol.Type == alcoholType);

        public void AddAlchohol(AlcoholType alcoholType, int count)
        {
            AlchoholDictionary addedAlc = alchohols.Find(a => a.alchohol.Type == alcoholType);
            if (addedAlc != null)
            {
                addedAlc.count += count;
            }

            Debug.Log($"<color=yellow>Added {addedAlc.alchohol.Name} +{count}</color>");
        }

        public void ReduceAlchohol(AlcoholType alcoholType, int count)
        {
            AlchoholDictionary addedAlc = alchohols.Find(a => a.alchohol.Type == alcoholType);
            if (addedAlc != null)
            {
                addedAlc.count -= count;
            }

            Debug.Log($"<color=yellow>Reduce {addedAlc.alchohol.Name} -{count}</color>");
        }

        public void Initialize() {
            InitAlchoholData();
        }

        private void InitAlchoholData() {

            AlcoholSO[] newAlcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol/DrinkSO/Alcohol");
            Array.Sort(newAlcohols, (a, b) => a.BuyCost.CompareTo(b.BuyCost));
            
            foreach ( AlcoholSO alcohol in newAlcohols ) {
                alchohols.Add(new AlchoholDictionary(alcohol));
            }
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






        //TOOD: it should read the SO instead of hardcode
        public IEnumerator MakeFood( NPCScript NPCScript, Order order ) {

            yield return new WaitForSeconds(10);

            Debug.Log("food ready");


        }

        public IEnumerator MakeDrink( NPCScript NPCScript, Order order ) 
        {
            yield return new WaitForSeconds(5);

            Debug.Log("drink ready");
        }
    }
}
