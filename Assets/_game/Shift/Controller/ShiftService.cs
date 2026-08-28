using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Bar.View;
using Assets._game.Core.StateMachine;
using Assets._game.Player.View;
using Assets._game.Shift.View;
using Assets._game.Sound.EnumInterface;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Shift.Controller
{
    public class ShiftService : IShiftService
    {
        IEconomyService economyService;
        DeskManagerView deskManagerView;
        [Inject] IGameStateMachine gameStateMachine;
        [Inject] ShiftManagerView shiftManagerView;

        [Inject]
        void Construct(IEconomyService economyService, DeskManagerView deskManagerView)
        {
            this.economyService = economyService;
            this.deskManagerView = deskManagerView;
        }

        int currentShift = 0;
        public int CurrentShift() => currentShift;

        bool firstStart = true;
        public void StartDayShift()
        {
            gameStateMachine.Enter<DayShiftState>();
            shiftManagerView.StopTimer();

            CheckLose();

            if (firstStart)
                economyService.AcceptMaintainingMoney();
            else
                firstStart = false;
        }

        public void StartNightShift() {
            gameStateMachine.Enter<NightShiftState>();
            //musicService.Play(MusicType.Night);

            shiftManagerView.StartTimer();

            currentShift++;
            deskManagerView.UpdateShiftText(currentShift);

            economyService.IncreaseQuota();
        }

        private void CheckLose()
        {
            if(economyService.QuotaCurrentValue() < economyService.QuotaMaxValue())
            {
                Debug.Log("<color=red>YOU LOSE!!</color>");
            }
        }
    }
}