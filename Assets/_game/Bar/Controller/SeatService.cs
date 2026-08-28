using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public class SeatService : ISeatService {

        List<Seat> seats = new();

        public event System.Action<int, int> OnSeatCountChanged;

        public int CurrentOccupiedSeats => seats.Count(s => s.IsOccupied && !s.IsBroken);
        public int MaxSeats => seats.Count(s => !s.IsBroken);
        public int AvailableSeats => seats.Count(s => !s.IsOccupied && !s.IsBroken);


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

            Debug.Log($"there is {seats.Count} seat");

            Seat bestSeat = null;
            float bestDistanceSqr = float.MaxValue;

            foreach ( Seat seat in seats ) {
                if ( seat == null ) {
                    Debug.LogWarning("Seat is NULL");
                    continue;
                }

                //Debug.Log(
                //    $"Seat: {seat.name} | " +
                //    $"Occupied: {seat.IsOccupied} | " +
                //    $"Broken: {seat.IsBroken}"
                //);

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

        public void RegisterSeat( Seat seat ) {
            if ( seat != null && !seats.Contains(seat) ) {
                seats.Add(seat);
                NotifyStateChanged();
            }
        }

        public void UnregisterSeat( Seat seat ) {
            if ( seat != null && seats.Remove(seat) ) {
                NotifyStateChanged();
            }
        }

        public bool TryReserveSeat( out Seat availableSeat ) {
            availableSeat = seats.FirstOrDefault(s => !s.IsOccupied && !s.IsBroken);

            if ( availableSeat != null && availableSeat.TryReserve() ) {
                NotifyStateChanged();
                return true;
            }

            availableSeat = null;
            return false;
        }

        public void ReleaseSeat( Seat seat ) {
            if ( seat != null ) {
                NotifyStateChanged();
            }
        }

        public void ReportSeatBroken( Seat seat ) => NotifyStateChanged();


        public void ReportSeatRepaired( Seat seat ) => NotifyStateChanged();

        private void NotifyStateChanged() => OnSeatCountChanged?.Invoke(CurrentOccupiedSeats, MaxSeats);
    }
}