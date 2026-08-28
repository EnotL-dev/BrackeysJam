using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Bar.View;
using Assets._game.Player.View;
using Assets._game.Sound.EnumInterface;
using Assets._game.Store.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Controller {
    public class EconomyService : IEconomyService {
        IBarService barService;
        ISFXService sFXService;
        PlayerInterfaceManagerView playerInterfaceManagerView;
        AlcoholCatalog alcoholCatalogSO;
        DeskManagerView deskManagerView;

        Dictionary<AlcoholType,int> alcoholDictionary;

        [Inject]
        void Construct(IBarService barService, ISFXService sFXService,
        PlayerInterfaceManagerView playerInterfaceManagerView,
        AlcoholCatalog alcoholCatalogSO, DeskManagerView deskManagerView) {
            this.barService = barService;
            this.sFXService = sFXService;
            this.playerInterfaceManagerView = playerInterfaceManagerView;
            this.alcoholCatalogSO = alcoholCatalogSO;
            this.deskManagerView = deskManagerView;
        }

        // QUOTA
        int quotaCurrentValue = 0;
        public int QuotaCurrentValue() => quotaCurrentValue;
        int quotaMaxValue = 15;
        public int QuotaMaxValue() => quotaMaxValue;
        // QUOTA

        public void AcceptMaintainingMoney()
        {
            playerInterfaceManagerView.AddMoney(_money, _money + quotaCurrentValue);

            _money += quotaCurrentValue;
            quotaCurrentValue = 0;
        }

        public void IncreaseQuota()
        {
            quotaMaxValue = (int)(quotaMaxValue * (1f + Random.Range(0.1f, 0.2f)));

            deskManagerView.UpdateQuotaText(0, quotaMaxValue);
            playerInterfaceManagerView.ReduceQuotaMoney(quotaCurrentValue, 0, quotaMaxValue);
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

            sFXService.Play(SFXType.CashIn);

            barService.AddAlcohol(alcoholType, count);
            _money -= cost;

            Debug.Log($"<color=blue>Bought {alcoholType} in count {count}</color>");
        }

        public void SellAlchohol( AlcoholType alcoholType, int count ) {
            if ( alcoholDictionary == null ) alcoholDictionary = barService.GetAlcoholDictionary();
            int cost = alcoholCatalogSO.Get(alcoholType).SoldCost * count;


            if ( count < 1 && alcoholDictionary[alcoholType] < count ) return;

            deskManagerView.UpdateQuotaText(quotaCurrentValue + cost, quotaMaxValue);
            playerInterfaceManagerView.AddQuotaMoney(quotaCurrentValue, quotaCurrentValue + cost, quotaMaxValue);

            barService.ReduceAlchohol(alcoholType, count);
            quotaCurrentValue += cost;

            Debug.Log(quotaCurrentValue);
        }

        public void BuyFurniture(int cost ) {
            playerInterfaceManagerView.ReduceMoney(_money, _money - cost);

            _money -= cost;
        }
    }
}