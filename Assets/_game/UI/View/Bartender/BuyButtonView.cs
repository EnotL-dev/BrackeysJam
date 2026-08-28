using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.Alcohol;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets._game.UI.View
{
    public class BuyButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        private Action<AlcoholType, int> BuyAction;

        private AlcoholType myAlcoholType;

        public void Initialize(AlcoholType alcoholType, Action<AlcoholType, int> buyAction)
        {
            myAlcoholType = alcoholType;
            BuyAction = buyAction;
        }

        public void BuyAlc()
        {
            int count = 1;
            if (inputField.text != "")
                count = Convert.ToInt32(inputField.text);

            BuyAction?.Invoke(myAlcoholType, count);
            inputField.text = "";
        }
    }
}
