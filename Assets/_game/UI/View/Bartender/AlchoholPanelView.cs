using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.UI.View
{
    public class AlchoholPanelView : MonoBehaviour
    {
        [Inject] IBarService barService;

        [SerializeField] private Image iconDrink;
        [SerializeField] private TextMeshProUGUI textNameDrink;
        [SerializeField] private TextMeshProUGUI textBuy;
        [SerializeField] private TextMeshProUGUI textSell;
        [SerializeField] private TextMeshProUGUI textCount;
        [SerializeField] private BuyButtonView buyButtonView;

        AlcoholType myType = AlcoholType.Beer;
        private AlchoholDictionary myAlchohol => barService.GetAlcoholDictionary(myType);
        public void Initialize(AlcoholType newType, Action<AlcoholType, int> buyAction)
        {
            myType = newType;
            buyButtonView.Initialize(myType, buyAction);
        }

        public void UpdateUI()
        {
            iconDrink.sprite = myAlchohol.alchohol.Icon;
            textNameDrink.text = myAlchohol.alchohol.Name;
            textBuy.text = myAlchohol.alchohol.BuyCost.ToString();
            textSell.text = myAlchohol.alchohol.SoldCost.ToString();
            textCount.text = myAlchohol.count.ToString();
        }
    }
}
