using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public interface IBarService {
        Vibe GetVibe();
        void AddVibe(int count);
        void ReduceVibe(int count);
        Dictionary<AlcoholType, int> GetAlcoholDictionary();
        void AddAlcohol( AlcoholType alcoholType, int count );
        void ReduceAlchohol( AlcoholType alcoholType, int count );
    }
}