using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public class OrderFactory  {
        public FoodOrder CreateFood( FoodType food ) {
            return new FoodOrder(food);
        }

        public DrinkOrder CreateDrink( DrinkType drink ) {
            return new DrinkOrder(drink);
        }

        public DrinkOrder CreateAlcohol( AlcoholType alcohol ) {
            return new DrinkOrder(
                DrinkType.Alcohol,
                alcohol
            );
        }
    }
}