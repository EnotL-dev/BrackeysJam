using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [System.Serializable]
    public class AlchoholDictionary
    {
        public int count = 0;
        public Alchohol alchohol;

        public AlchoholDictionary(Alchohol alchohol) 
        {
            this.alchohol = alchohol;
        }
    }
}