using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.UI.View {
    public class AlchoholPanelView : MonoBehaviour {
        IBarService barService;
        AlcoholCatalog alcoholCatalogSO;


        [SerializeField] private Image iconDrink;
        [SerializeField] private TextMeshProUGUI textNameDrink;
        [SerializeField] private TextMeshProUGUI textBuy;
        [SerializeField] private TextMeshProUGUI textSell;
        [SerializeField] private TextMeshProUGUI textCount;
        [SerializeField] private BuyButtonView buyButtonView;



        AlcoholType myType = AlcoholType.Beer;

        [Inject]
        void Construct( IBarService barService,
            AlcoholCatalog alcoholCatalogSO ) {
            this.barService = barService;
            this.alcoholCatalogSO = alcoholCatalogSO;
        }

        private Dictionary<AlcoholType, int> myAlchohol => barService.GetAlcoholDictionary();


        public void Initialize( AlcoholType newType, Action<AlcoholType, int> buyAction ) {
            myType = newType;
            buyButtonView?.Initialize(myType, buyAction);
        }

        public void UpdateUI() {
            AlcoholSO alcoholData = alcoholCatalogSO.Get(myType);
            if ( alcoholData == null ) return;

            int count = myAlchohol[myType];

            if ( iconDrink != null ) iconDrink.sprite = alcoholData.Icon;
            if ( textNameDrink != null ) textNameDrink.text = alcoholData.Name;
            if ( textBuy != null ) textBuy.text = $"{alcoholData.BuyCost}$";
            if ( textSell != null ) textSell.text = $"{alcoholData.SoldCost}$";
            if ( textCount != null ) textCount.text = count.ToString();
        }
    }
}
