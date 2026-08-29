using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol;
using Assets._game.Bar.View;
using Assets._game.Core.StateMachine;
using Assets._game.Hint.Controller;
using Assets._game.Hint.Model;
using Assets._game.Player.View;
using Assets._game.Shift.View;
using Assets._game.Sound.Controller;
using Assets._game.Sound.EnumInterface;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Shift.Controller
{
    public class ShiftService : IShiftService
    {
        //[Inject] MusicService musicService;
        IEconomyService economyService;
        DeskManagerView deskManagerView;
        [Inject] IGameStateMachine gameStateMachine;
        [Inject] ShiftManagerView shiftManagerView;
        [Inject] IHintService hintService;

        [Inject]
        void Construct(IEconomyService economyService, DeskManagerView deskManagerView)
        {
            this.economyService = economyService;
            this.deskManagerView = deskManagerView;
        }

        int currentShift = 0;
        public int CurrentShift() => currentShift;

        public void StartDayShift()
        {
            gameStateMachine.Enter<DayShiftState>();
            shiftManagerView.StopTimer();
            //musicService.Play(MusicType.Day);

            CheckLose();
            economyService.AcceptMaintainingMoney();

            hintService.RemoveHint(HintType.CompleteFirstQouta);
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