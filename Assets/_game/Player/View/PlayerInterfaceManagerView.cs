using Assets._game.UI.Controller;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._game.Player.View
{
    public class PlayerInterfaceManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textSeats;
        int currentSeats = 0;
        int maxSeats = 5;

        [Space(5)]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color reduceColor;
        [SerializeField] private TextMeshProUGUI textMoney;
        [SerializeField] private TextMeshProUGUI textQuotaMoney;
        [SerializeField] private TextMeshProUGUI textShiftTimer;

        [SerializeField] private InputActionReference testInput;

        public void Start()
        {
            textMoney.text = "10,000 $";
            textQuotaMoney.text = "0$ / 0$";

            textSeats.text = $"{currentSeats} / {maxSeats}";

            textShiftTimer.text = "--:--";
        }

        private void OnEnable()
        {
            testInput.action.Enable();
        }

        private void OnDisable()
        {
            testInput.action.Disable();
        }

        private void Update()
        {
            if (testInput.action.WasPressedThisFrame())
            {
                
            }
        }

        public void AddQuotaMoney(int startCount, int endCount, int quotaMax)
        {
            DOTween.Kill(textMoney.gameObject);
            textQuotaMoney.AnimateQuotaText(startCount, endCount, quotaMax, 1f);
        }

        public void ReduceQuotaMoney(int startCount, int endCount, int quotaMax)
        {
            DOTween.Kill(textMoney.gameObject);
            textQuotaMoney.AnimateQuotaText(startCount, endCount, quotaMax, 1f);
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

        public void AddCurrentSeats(int count) // If visitor accepted in bar
        {
            DOTween.Kill(textSeats.gameObject);
            currentSeats += count;
            textSeats.text = $"{currentSeats} / {maxSeats}";
            textSeats.transform.DOScale(1.3f, 0.2f).SetEase(Ease.OutBack).OnComplete(() => textSeats.transform.DOScale(1f, 0.3f).SetEase(Ease.InBack));
        }

        public void AddMaxSeats(int count) // If player buy and setup new seat
        {
            DOTween.Kill(textSeats.gameObject);
            maxSeats += count;
            textSeats.text = $"{currentSeats} / {maxSeats}";
            textSeats.transform.DOScale(1.3f, 0.2f).SetEase(Ease.OutBack).OnComplete(() => textSeats.transform.DOScale(1f, 0.3f).SetEase(Ease.InBack));
        }

        public void ReduceCurrentSeats(int count) // If someone leave it
        {
            DOTween.Kill(textSeats.gameObject);
            if(currentSeats > 1)
                currentSeats -= count;
            textSeats.text = $"{currentSeats} / {maxSeats}";
            textSeats.transform.DOScale(1.3f, 0.2f).SetEase(Ease.OutBack).OnComplete(() => textSeats.transform.DOScale(1f, 0.3f).SetEase(Ease.InBack));
        }

        public void ReduceMaxSeats(int count) // If someone broke it
        {
            DOTween.Kill(textSeats.gameObject);
            if (maxSeats > 1)
                maxSeats -= count;
            textSeats.text = $"{currentSeats} / {maxSeats}";
            textSeats.transform.DOScale(1.3f, 0.2f).SetEase(Ease.OutBack).OnComplete(() => textSeats.transform.DOScale(1f, 0.3f).SetEase(Ease.InBack));
        }

        public void UpdateTimer(int seconds)
        {
            string timeString = string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
            textShiftTimer.text = timeString;

            if(seconds % 60 ==0 || seconds <= 30)
            {
                DOTween.Kill(textShiftTimer.gameObject);
                textShiftTimer.AnimateTimerShake();
            }
        }

        public void StopTimer()
        {
            textShiftTimer.text = "--:--";
        }
    }
}
