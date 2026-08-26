using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Player.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller
{
    public class EconomyService : IEconomyService
    {
        [Inject] IBarService barService;
        [Inject] PlayerInterfaceManagerView playerInterfaceManagerView;

        private int _money = 10000;
        public int Money { get => _money; }

        public void BuyAlchohol(AlcoholType alcoholType, int count)
        {
            AlchoholDictionary alchoholDictionary = barService.GetAlcoholDictionary(alcoholType);
            int cost = alchoholDictionary.alchohol.BuyCost * count;
            if (count < 1 && cost > _money)
            {
                Debug.Log($"<color=red>Cant buy {alcoholType} in count {count}</color>");
                return;
            }

            playerInterfaceManagerView.ReduceMoney(_money, _money-cost);

            barService.AddAlchohol(alcoholType, count);
            _money -= cost;

            Debug.Log($"<color=blue>Bought {alcoholType} in count {count}</color>");
        }

        public void SellAlchohol(AlcoholType alcoholType, int count)
        {
            AlchoholDictionary alchoholDictionary = barService.GetAlcoholDictionary(alcoholType);
            if (count < 1 && alchoholDictionary.count < count) return;

            barService.ReduceAlchohol(alcoholType, count);
            _money += alchoholDictionary.alchohol.SoldCost * count;
        }
    }
}