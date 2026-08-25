using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model {
    public abstract class Order {

        public OrderType Type { get; }
        public Order( OrderType type ) {
            Type = type;
        }

    }
}