using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller
{
    public interface IBarService
    {
        AlchoholDictionary GetAlcoholDictionary(AlcoholType alcoholType);
        void AddAlchohol(AlcoholType alcoholType, int count);
        void ReduceAlchohol(AlcoholType alcoholType, int count);
    }
}