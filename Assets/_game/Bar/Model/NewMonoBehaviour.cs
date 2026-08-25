using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model {
    public class FoodOrder : Order {

        public FoodType Food { get; }

        public FoodOrder( FoodType food ) : base(OrderType.Food) {
            Food = food;
        }


    }
}