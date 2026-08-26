using Assets._game.Bar.Model.Alcohol;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller
{
    public interface IEconomyService
    {
        int Money { get; }

        void BuyAlchohol(AlcoholType alcoholType, int count = 1);
        void SellAlchohol(AlcoholType alcoholType, int count = 1);
    }
}