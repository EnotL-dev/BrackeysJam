using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.SOScript.DrinkSO;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model {
    public class AlcoholOrder : DrinkOrder {

        public AlcoholType alcoholType { get; }

        public AlcoholOrder( AlcoholType type ) : base(DrinkType.Alcohol) {
            this.alcoholType = type;
        }
    }
}