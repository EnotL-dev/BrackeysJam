using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [CreateAssetMenu(fileName = "OrderItemSO", menuName = "OrderItemSO/DrinkSO/AlcoholSO")]
    public class AlcoholSO : DrinkSO
    {
        [SerializeField] private AlcoholType type;
        
        [SerializeField] private string effect = "nothing";

        public AlcoholType Type => type;
        public string Effect => effect;
    }
}