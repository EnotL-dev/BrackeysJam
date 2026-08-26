using Assets._game.UI.Controller;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets._game.Player.View
{
    public class PlayerInterfaceManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textShift;
        [SerializeField] private TextMeshProUGUI textSeats;
        [Space(5)]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color reduceColor;
        [SerializeField] private TextMeshProUGUI textMoney;
        [SerializeField] private TextMeshProUGUI textShiftTimer;

        public void Start()
        {
            textMoney.text = "10,000 $";

            textShift.text = "0";
            textSeats.text = "0 / 5";

            textShiftTimer.text = "--:--";
        }

        public void AddMoney(int startCount, int endCount)
        {
            DOTween.Kill(textMoney.gameObject);
            textMoney.AnimateIncrease(startCount, endCount, 1f);
        }

        public void ReduceMoney(int startCount, int endCount)
        {
            DOTween.Kill(textMoney.gameObject);
            textMoney.AnimateDecrease(startCount, endCount, 0.5f, reduceColor, normalColor);
        }
    }
}
