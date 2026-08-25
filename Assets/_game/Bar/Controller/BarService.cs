using Assets._game.Bar.Model;
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

        private List<AlchoholDictionary> alchoholDictionary;

        public void Initialize()
        {
            InitAlchoholData();
        }

        private void InitAlchoholData()
        {
            alchoholDictionary = new List<AlchoholDictionary>();

            AlchoholData alchoholData = Resources.Load<AlchoholData>("Bar/Alchohol/AlchoholData");
            foreach (Alchohol alchohol in alchoholData.alchohols)
            {
                alchoholDictionary.Add(new AlchoholDictionary(alchohol));
            }
        }

        public IEnumerator RequestOrder (NPCScript NPCScript ,OrderType order ) {
            switch ( order ) {
                case OrderType.Food:
                    yield return MakeFood();
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