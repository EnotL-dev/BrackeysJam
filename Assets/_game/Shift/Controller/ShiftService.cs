using Assets._game.Core.StateMachine;
using Assets._game.Shift.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Shift.Controller
{
    public class ShiftService : IShiftService
    {
        [Inject] private IGameStateMachine gameStateMachine;
        [Inject] private ShiftManagerView shiftManagerView;

        int currentShift = 0;
        public int CurrentShift() => currentShift;

        public void StartDayShift()
        {
            gameStateMachine.Enter<DayShiftState>();
            shiftManagerView.StopTimer();
        }

        public void StartNightShift()
        {
            gameStateMachine.Enter<NightShiftState>();
            shiftManagerView.StartTimer();

            currentShift++;
            shiftManagerView.SetShiftCount(currentShift);
        }
    }
}