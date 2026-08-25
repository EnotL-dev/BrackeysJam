using Assets._game.Bar.Model.SOScript.DrinkSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model.SOScript.FoodSO {
    public abstract class DrinkSO : OrderItemSO {

        [Tooltip("please ref to the correct the enum (if water then set water) else dont care")]
        [SerializeField] private DrinkType drinkType;

        public DrinkType Type => drinkType;
    }
}