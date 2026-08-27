using Assets._game.Core.StateMachine;
using Assets._game.Shift.View;
using Assets._game.Sound.EnumInterface;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Shift.Controller {
    public class ShiftService : IShiftService {
        private IGameStateMachine gameStateMachine;
        private IMusicService musicService;
        private ShiftManagerView shiftManagerView;

        [Inject]
        void Construct( IGameStateMachine gameStateMachine,
            IMusicService musicService,
            ShiftManagerView shiftManagerView ) {
            this.gameStateMachine = gameStateMachine;
            this.musicService = musicService;
            this.shiftManagerView = shiftManagerView;
        }

        int currentShift = 0;
        public int CurrentShift() => currentShift;

        public void StartDayShift() {
            gameStateMachine.Enter<DayShiftState>();
            shiftManagerView.StopTimer();
            musicService.Play(MusicType.Day);
        }

        public void StartNightShift() {
            gameStateMachine.Enter<NightShiftState>();
            musicService.Play(MusicType.Night);

            shiftManagerView.StartTimer();

            currentShift++;
            shiftManagerView.SetShiftCount(currentShift);
        }
    }
}