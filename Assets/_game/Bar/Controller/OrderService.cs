using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public class OrderService{

        public Order CreateFoodOrder( FoodType food ) {
            return new Order(
                OrderType.Food,
                food: food
            );
        }

        public Order CreateWaterOrder() {
            return new Order(
                OrderType.Drink,
                drink: DrinkType.Water
            );
        }

        public Order CreateAlcoholOrder( AlcoholType alcohol ) {
            return new Order(
                OrderType.Drink,
                drink: DrinkType.Alcohol,
                alcohol: alcohol
            );
        }


    }
}