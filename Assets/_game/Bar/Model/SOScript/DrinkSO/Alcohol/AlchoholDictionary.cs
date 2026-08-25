using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [System.Serializable]
    public class AlchoholDictionary
    {
        public int count = 0;
        public AlcoholSO alchohol;

        public AlchoholDictionary( AlcoholSO alchohol ) 
        {
            this.alchohol = alchohol;
        }
    }
}