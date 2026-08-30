using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model {
    public class AlcoholOrder : DrinkOrder {
        public int amount;
        public AlcoholType alcoholType { get; }

        public AlcoholOrder( AlcoholType type, int amount ) : base(DrinkType.Alcohol) {
            this.amount = amount;
            this.alcoholType = type;
        }
    }
}