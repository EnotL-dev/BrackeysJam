using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model {
    public class FoodOrder : Order {

        public FoodType Type { get; }

        public FoodOrder( FoodType type ) : base(OrderType.Food) {
            Type = type;
        }




    }
}