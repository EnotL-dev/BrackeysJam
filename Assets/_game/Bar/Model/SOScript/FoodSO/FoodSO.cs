using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model.SOScript.FoodSO {
    public abstract class FoodSO : OrderItemSO{

        [SerializeField] private FoodType foodType;


        public FoodType Type => foodType;

    }
}