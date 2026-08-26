using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.TestingScript;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller {
    public class BarService : IBarService, IInitializable {

        IEconomyService economyService;
        WaitingLineService waitingLineService;
        SeatService seatService;
        AlcoholCatalog alcoholCatalogSO;

        Dictionary<AlcoholType,int> alcohols = new();


        [Inject]
        void Construct( IEconomyService economyService,
            [Inject(Id = "Bar")] WaitingLineService waitingLine,
            SeatService seatService,
            AlcoholCatalog alcoholCatalogSO) {
            this.economyService = economyService;
            this.waitingLineService = waitingLine;
            this.seatService = seatService;
            this.alcoholCatalogSO = alcoholCatalogSO;
        }

        public void Initialize() {
            InitAlchoholData();
        }

        private void InitAlchoholData() {

            AlcoholSO[] newAlcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol/DrinkSO/Alcohol");
            Array.Sort(newAlcohols, ( a, b ) => a.BuyCost.CompareTo(b.BuyCost));

            foreach ( AlcoholSO alcohol in newAlcohols ) {
                alcohols.Add(alcohol.AlcoholType, 0);
            }
        }

        public Dictionary<AlcoholType, int> GetAlcoholDictionary() => alcohols;

        public void AddAlcohol( AlcoholType alcoholType, int count ) {

            alcohols[alcoholType] += count;

            Debug.Log($"<color=yellow>Added {alcoholType} +{count}</color>");
        }

        public void ReduceAlchohol( AlcoholType alcoholType, int count ) {
            alcohols[alcoholType] -= count;

            Debug.Log($"<color=yellow>Reduce {alcoholType} -{count}</color>");
        }





        //public IEnumerator RequestOrder( NPCScript NPCScript, Order order, Action onOrderReady ) {
        //    switch ( order ) {
        //        case FoodOrder:
        //            yield return MakeFood(NPCScript, order);
        //            break;

        //        case DrinkOrder:
        //            yield return MakeDrink(NPCScript, order);
        //            break;

        //        default:
        //            yield return MakeFood(NPCScript, null);
        //            break;
        //    }

        //    onOrderReady?.Invoke();
        //}






        ////TOOD: it should read the SO instead of hardcode
        //public IEnumerator MakeFood( NPCScript NPCScript, Order order ) {

        //    yield return new WaitForSeconds(10);

        //    Debug.Log("food ready");


        //}

        //public IEnumerator MakeDrink( NPCScript NPCScript, Order order ) {
        //    yield return new WaitForSeconds(5);

        //    Debug.Log("drink ready");


        //}


        public IEnumerator RequestDrink( AlcoholOrder alcoholorder, Action onOrderReady ) {
            var so = alcoholCatalogSO.Get(alcoholorder.alcoholType);

            Debug.Log($"Order {alcoholorder.alcoholType}");
            yield return new WaitForSeconds(so.PrepareTime);

            economyService.SellAlchohol(alcoholorder.alcoholType);
            onOrderReady?.Invoke();
        }

    }
}
