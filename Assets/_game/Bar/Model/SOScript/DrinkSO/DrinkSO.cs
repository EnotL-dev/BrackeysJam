using Assets._game.Bar.Model.SOScript.DrinkSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model.SOScript.FoodSO {
    public abstract class DrinkSO : ScriptableObject {

        [SerializeField] private DrinkType drinkType;

        public DrinkType Type => drinkType;
    }
}