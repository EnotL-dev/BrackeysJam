using Assets._game.Player.View;
using Assets._game.Shift.Controller;
using UnityEngine;
using Zenject;

namespace Assets._game.Shift.View
{
    public class ShiftManagerView : MonoBehaviour
    {
        [Inject] IShiftService shiftService;
        [Inject] PlayerInterfaceManagerView playerInterfaceManagerView;

        [SerializeField] private int ShiftCycleTime = 300; // time of shift in seconds

        private bool enabledTimer = false;
        private float timer = 0;
        private int seconds = 0;
        public int GetSeconds() => seconds;

        void Update()
        {
            if (!enabledTimer) return;

            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = 0f;
                seconds--;
                playerInterfaceManagerView.UpdateTimer(seconds);

                if (seconds <= 0)
                    shiftService.StartDayShift();
            }
        }

        public void StartTimer()
        {
            enabledTimer = true;
            timer = 1;
            seconds = ShiftCycleTime;
        }

        public void StopTimer()
        {
            playerInterfaceManagerView.StopTimer();
            enabledTimer = false;
        }

        public void SetShiftCount(int count)
        {
            playerInterfaceManagerView.SetShiftCount(count);
        }
    }
}