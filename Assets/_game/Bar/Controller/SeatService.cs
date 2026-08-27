using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public class SeatService {

        List<Seat> seats = new();


        public void InitializeListSeat( List<Seat> list ) {
            seats = list;
        }

        /// <summary>
        /// return a random Seat
        /// Depreciated Method, pass pos to find the best seat
        /// </summary>
        /// <returns></returns>
        public Seat FindBestSeat() {
            if ( seats.Count == 0 ) {
                Debug.LogWarning("There is no Seat");
                return null;
            }

            int index = Random.Range(0, seats.Count);
            return seats[index];
        }

        //find base on best distance
        public Seat FindBestSeat( Vector3 pos ) {

            if ( seats.Count == 0 ) {
                Debug.LogWarning("There is no Seat");
                return null;
            }

            Seat bestSeat = null;
            float bestDistanceSqr = float.MaxValue;

            foreach ( Seat seat in seats ) {
                if ( seat == null ) {
                    Debug.LogWarning("Seat is NULL");
                    continue;
                }

                Debug.Log(
                    $"Seat: {seat.name} | " +
                    $"Occupied: {seat.IsOccupied} | " +
                    $"Broken: {seat.IsBroken}"
                );

                if ( seat.IsOccupied || seat.IsBroken ) continue;

                float distanceSqr = (seat.transform.position - pos).sqrMagnitude;

                if ( distanceSqr < bestDistanceSqr ) {
                    bestDistanceSqr = distanceSqr;
                    bestSeat = seat;
                }
            }

            if ( bestSeat == null ) {
                Debug.LogWarning("Can't find best seat");
                return null;
            }

            if ( bestSeat != null ) bestSeat.TryReserve();

            return bestSeat;
        }



    }
}