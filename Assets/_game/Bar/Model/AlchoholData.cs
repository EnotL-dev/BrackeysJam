using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [CreateAssetMenu(fileName = "AlchoholData", menuName = "Alchohol/AlchoholData")]
    public class AlchoholData : ScriptableObject
    {
        public List<Alchohol> alchohols = new List<Alchohol>();
    }
}