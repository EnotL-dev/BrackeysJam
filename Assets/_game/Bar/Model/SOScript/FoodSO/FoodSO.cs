using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model.SOScript.FoodSO {
    [CreateAssetMenu(fileName = "OrderItemSO", menuName = "OrderItemSO/FoodSO")]
    public class FoodSO : OrderItemSO{

        [SerializeField] private FoodType foodType;


        public FoodType Type => foodType;

    }
}