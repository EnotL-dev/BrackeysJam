using Assets._game.Bar.Controller;
using Assets._game.Hint.Model;
using Assets._game.UI.Controller;
using Assets._game.UI.View;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.Player.View
{
    public class PlayerInterfaceManagerView : MonoBehaviour
    {
        [Inject] ISeatService seatService;

        [SerializeField] private TextMeshProUGUI textSeats;
        [Space(5)]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color reduceColor;
        [SerializeField] private TextMeshProUGUI textMoney;
        [SerializeField] private TextMeshProUGUI textQuotaMoney;
        [SerializeField] private TextMeshProUGUI textShiftTimer;
        [Space(5)]
        [SerializeField] private VerticalLayoutGroup prentHintPanel;
        [SerializeField] private HintPanel prefabHintPanel;
        [Space(5)]
        [SerializeField] private RectTransform prefabAddMoney;

        public void Start()
        {
            textMoney.text = "200 $";
            textQuotaMoney.text = "0$ / 0$";

            textSeats.text = $"0 / 5";

            textShiftTimer.text = "--:--";

            seatService.OnSeatCountChanged += UpdateSeatsText;
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

            RectTransform instance = Instantiate(prefabAddMoney, textMoney.transform);
            instance.localPosition = Vector3.zero;

            TextMeshProUGUI tmp = instance.GetComponent<TextMeshProUGUI>();
            tmp.text = $"{endCount-startCount} $";
            tmp.color = normalColor;

            tmp.DOFade(1, 0.3f).SetEase(Ease.OutQuad);
            instance.DOLocalMoveY(60, 0.2f).SetEase(Ease.OutQuad);    
            tmp.DOFade(0, 0.3f).SetDelay(1.2f).OnComplete(() => Destroy(instance.gameObject));
        }

        public void ReduceMoney(int startCount, int endCount)
        {
            DOTween.Kill(textMoney.gameObject);
            textMoney.AnimateDecrease(startCount, endCount, 0.5f, reduceColor, normalColor);

            RectTransform instance = Instantiate(prefabAddMoney, textMoney.transform);
            instance.localPosition = Vector3.zero;

            TextMeshProUGUI tmp = instance.GetComponent<TextMeshProUGUI>();
            tmp.text = $"{endCount - startCount} $";
            tmp.color = reduceColor;

            tmp.DOFade(1, 0.3f).SetEase(Ease.OutQuad);
            instance.DOLocalMoveY(60, 0.2f).SetEase(Ease.OutQuad);
            tmp.DOFade(0, 0.3f).SetDelay(1.2f).OnComplete(() => Destroy(instance.gameObject));
        }

        /*
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
        */
        
        public void UpdateSeatsText(int current, int max)
        {
            textSeats.text = $"{current} / {max}";
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

        List<HintPanel> spawnedHints = new List<HintPanel>();
        public void AddHint(HintSO hintSO)
        {
            HintPanel newHintPanel = Instantiate(prefabHintPanel);
            newHintPanel.transform.SetParent(prentHintPanel.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(prentHintPanel.GetComponent<RectTransform>());

            RectTransform panelRect = newHintPanel.GetComponent<RectTransform>();
            panelRect.DOAnchorPosY(panelRect.anchoredPosition.y, 0.5f).From(new Vector2(panelRect.anchoredPosition.x, panelRect.anchoredPosition.y - 50f)).SetEase(Ease.OutBack);
            foreach (var img in newHintPanel.GetComponentsInChildren<Image>()) img.DOFade(1f, 0.5f).From(0f);

            newHintPanel.Initialize(hintSO.Title(), hintSO.HintType);
            spawnedHints.Add(newHintPanel);
        }

        public void RemoveHint(HintType hintType)
        {
            foreach(HintPanel hintPanel in spawnedHints)
            {
                if(hintPanel.hintType == hintType)
                {
                    DOTween.Kill(hintPanel);
                    Destroy(hintPanel.gameObject);

                    spawnedHints.Remove(hintPanel);
                    break;
                }
            }
        }
    }
}
