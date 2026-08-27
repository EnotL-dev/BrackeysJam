namespace Assets._game.Shift.Controller
{
    public interface IShiftService
    {
        int CurrentShift();
        void StartDayShift();
        void StartNightShift();
    }
}