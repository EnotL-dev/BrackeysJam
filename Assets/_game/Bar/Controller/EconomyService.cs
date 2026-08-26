using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Player.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller {
    public class EconomyService : IEconomyService {
        IBarService barService;
        PlayerInterfaceManagerView playerInterfaceManagerView;
        AlcoholCatalog alcoholCatalogSO;


        Dictionary<AlcoholType,int> alcoholDictionary;

        [Inject]
        void Construct( IBarService barService,
        PlayerInterfaceManagerView playerInterfaceManagerView,
        AlcoholCatalog alcoholCatalogSO ) {
            this.barService = barService;
            this.playerInterfaceManagerView = playerInterfaceManagerView;
            this.alcoholCatalogSO = alcoholCatalogSO;
        }

        private int _money = 10000;
        public int Money { get => _money; }

        public void BuyAlchohol( AlcoholType alcoholType, int count ) {
            if ( alcoholDictionary == null ) alcoholDictionary = barService.GetAlcoholDictionary();
            int cost = alcoholCatalogSO.Get(alcoholType).BuyCost * count;

            if ( count < 1 && cost > _money ) {
                Debug.Log($"<color=red>Cant buy {alcoholType} in count {count}</color>");
                return;
            }

            playerInterfaceManagerView.ReduceMoney(_money, _money - cost);

            barService.AddAlcohol(alcoholType, count);
            _money -= cost;

            Debug.Log($"<color=blue>Bought {alcoholType} in count {count}</color>");
        }

        public void SellAlchohol( AlcoholType alcoholType, int count ) {
            if ( alcoholDictionary == null ) alcoholDictionary = barService.GetAlcoholDictionary();
            int cost = alcoholCatalogSO.Get(alcoholType).SoldCost * count;


            if ( count < 1 && alcoholDictionary[alcoholType] < count ) return;

            barService.ReduceAlchohol(alcoholType, count);
            _money += cost;

            Debug.Log(_money);
        }
    }
}