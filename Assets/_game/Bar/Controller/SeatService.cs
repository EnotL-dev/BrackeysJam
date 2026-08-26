using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public class SeatService {

        List<Seat> seats = new();


        public void InitializeListSeat( List<Seat> list ) {
            seats = list;
        }

        public Seat FindBestSeat() {

            //random for now

            if ( seats.Count == 0 ) {
                Debug.LogWarning("There is no Seat");
                return null;
            }

            int index = Random.Range(0, seats.Count);
            return seats[index];
        }

        //find base on best distance
        public Seat FindBestSeat( Vector3 pos ) {
            return seats[0];
        }

    }
}