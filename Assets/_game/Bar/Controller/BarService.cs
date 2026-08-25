using Assets._game.Bar.Model;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Npc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller
{
    public class BarService : IBarService, IInitializable
    {
        SeatService seatService;

        [Inject]
        void Construct(SeatService seatService) {
            this.seatService = seatService;
        }

        //private List<AlchoholDictionary> alchoholDictionary;
        AlcoholSO[] alcohols;


        public void Initialize()
        {
            InitAlchoholData();
        }

        private void InitAlchoholData()
        {

            alcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol");

            foreach ( AlcoholSO alcohol in alcohols ) {
                Debug.Log($"{alcohol.Type} - {alcohol.BuyCost}");
            }

            //Debug.Log(alchoholDictionary.Count);
        }

        public IEnumerator RequestOrder (NPCScript NPCScript ,OrderType order ) {
            switch ( order ) {
                case OrderType.Food:
                    yield return MakeFood(order.);
                    break;

                case OrderType.Drink:
                    yield return MakeDrink();
                    break;
            }
        }

        





        public IEnumerator MakeFood() {
            

            yield return new WaitForSeconds(10);

            Debug.Log("food ready");
        }

        public IEnumerator MakeDrink() {
            yield return new WaitForSeconds(5);

            Debug.Log("drink ready");
        }
    }
}