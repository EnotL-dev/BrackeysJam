using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model
{
    [System.Serializable]
    public class Alchohol : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;
        [SerializeField] private string _name = "none";
        public string Name => _name;
        [SerializeField] private int cost = 5;
        public int Cost => cost;
        [SerializeField] private string effect = "nothing";
        public string Effect => effect;
    }
}