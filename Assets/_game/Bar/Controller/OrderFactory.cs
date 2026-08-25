using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using Assets._game.Bar.Model.SOScript.FoodSO;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public class OrderFactory {

        OrderType[] orderTypes;
        FoodType[] foodTypes;
        DrinkType[] drinkTypes;
        AlcoholType[] alcoholTypes;

        public FoodOrder CreateFood( FoodType food ) {
            return new FoodOrder(food);
        }

        public DrinkOrder CreateDrink( DrinkType drink ) {
            return new DrinkOrder(drink);
        }

        public AlcoholOrder CreateAlcoholOrder( AlcoholType type ) {
            return new AlcoholOrder(type);
        }


        //public WaterOrder CreateWaterOrder() {
        //    return new WaterOrder();
        //}



        public Order CreateRandomOrder() {
            if ( orderTypes == null ) {
                orderTypes = (OrderType[])System.Enum.GetValues(typeof(OrderType));
            }
            int index = Random.Range(0, orderTypes.Length); // this shuold be 2: 0 for food, 2 for drink
            return index == 0 ?
                CreateRandomFoodOrder() :
                CreateRandomDrinkOrder();
        }

        public DrinkOrder CreateRandomDrinkOrder() {
            if ( drinkTypes == null ) {
                drinkTypes = (DrinkType[])System.Enum.GetValues(typeof(DrinkType));
            }

            int index = Random.Range(0, drinkTypes.Length);

            switch ( index ) {
                case 0:
                    return CreateRandomAlcoholOrder();
                case 1:
                    Debug.Log("I turn off water for now, if see this mean bug");
                    return null;
                default:
                    Debug.Log("There some thing broken in request order");
                    return null;
            }

        }

        public AlcoholOrder CreateRandomAlcoholOrder() {
            if ( alcoholTypes == null ) {
                alcoholTypes = (AlcoholType[])System.Enum.GetValues(typeof(AlcoholType));
            }

            int index = Random.Range(0, alcoholTypes.Length);
            return new AlcoholOrder(alcoholTypes[index]);
        }

        public FoodOrder CreateRandomFoodOrder() {
            if ( foodTypes == null ) {
                foodTypes = (FoodType[])System.Enum.GetValues(typeof(FoodType));
            }

            int index = Random.Range(0, foodTypes.Length);

            return new FoodOrder(foodTypes[index]);

        }

    }
}
