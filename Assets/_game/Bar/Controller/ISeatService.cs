using System;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Controller {
    public interface ISeatService {

        int CurrentOccupiedSeats { get; }
        int MaxSeats { get; }
        int AvailableSeats { get; }

        event Action<int, int> OnSeatCountChanged;

        Seat FindBestSeat( Vector3 pos);

        void RegisterSeat( Seat seat );
        void UnregisterSeat( Seat seat );

        bool TryReserveSeat( out Seat availableSeat );
        void ReleaseSeat( Seat seat );
        void ReportSeatBroken( Seat seat );
        void ReportSeatRepaired( Seat seat );
    }
}