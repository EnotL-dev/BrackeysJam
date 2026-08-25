using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [CreateAssetMenu(fileName = "AlchoholData", menuName = "Alchohol/AlchoholData")]
    public class AlchoholData : ScriptableObject
    {
        public List<AlcoholSO> alchohols = new List<AlcoholSO>();
    }
}