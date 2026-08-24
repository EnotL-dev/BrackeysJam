using Assets._game.Bar.Controller;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.View {
    public class BarView : MonoBehaviour, IBarService {

        [SerializeField] SeatService seatService; //might use DI



        public void MakeFood() {

        }

        public void MakeDrink() {

        }


    }
}