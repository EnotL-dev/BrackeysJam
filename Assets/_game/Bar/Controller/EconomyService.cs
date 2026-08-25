using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller
{
    public class EconomyService : IEconomyService
    {
        private int _money = 0;
        public int Money { get => _money; }


    }
}