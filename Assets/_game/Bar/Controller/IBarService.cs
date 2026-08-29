using Assets._game.Bar.Model.Alcohol;
using Assets._game.Bar.Model.BarStatus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public interface IBarService {
        Vibe GetVibe();
        //This shuold use bar status instead
        void AddVibe(int count);
        void ReduceVibe(int count);
        ChaosStatus GetChaosStatus();
        void AddChaos(float amt);
        void ReduceChaos(float amt);

        Dictionary<AlcoholType, int> GetAlcoholDictionary();
        void AddAlcohol( AlcoholType alcoholType, int count );
        void ReduceAlchohol( AlcoholType alcoholType, int count );

        event Action<float, Action> OnNpcRequestBar;
    }
}