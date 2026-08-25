using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [System.Serializable]
    public class AlcoholSO : DrinkSO
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private AlcoholType type;
        [SerializeField] private int buyCost;
        [SerializeField] private int soldCost;
        [SerializeField] private float prepareTime;
        [SerializeField] private string effect = "nothing";

        public Sprite Icon => _icon;
        public AlcoholType Type => type;
        public int BuyCost  => buyCost;
        public int SoldCost => soldCost;
        public float PrepareTime => prepareTime;
        public string Effect => effect;
    }
}