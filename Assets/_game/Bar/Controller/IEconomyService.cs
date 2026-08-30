using Assets._game.Bar.Model.Alcohol;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller
{
    public interface IEconomyService
    {
        int QuotaCurrentValue();
        int QuotaMaxValue();
        void IncreaseQuota();
        void AcceptMaintainingMoney(); // when day start
        int Money { get; }

        void BuyAlchohol(AlcoholType alcoholType, int count = 1);
        void SellAlchohol(AlcoholType alcoholType, int count = 1);
        void BuyFurniture(int cost);

        Action<int> NotifySell { get; set; }
    }
}