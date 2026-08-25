using Assets._game.Bar.Model.SOScript.DrinkSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model {
    public class DrinkOrder : Order {
        public DrinkType Type { get; }

        public DrinkOrder( DrinkType type ) : base(OrderType.Drink) {
            Type = type;
        }
    }
}