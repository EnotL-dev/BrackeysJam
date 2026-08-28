using DG.Tweening;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Assets._game.UI.Controller
{
    public static class TextAnimations
    {
        private static string FormatValue(float value)
        {
            return Mathf.RoundToInt(value).ToString("N0", CultureInfo.InvariantCulture) + " $";
        }

        public static void AnimateDecrease(this TextMeshProUGUI text, float from, float to, float duration, Color reduceColor, Color normalColor)
        {
            text.color = reduceColor;
            float current = from;
            text.text = FormatValue(current);

            Sequence seq = DOTween.Sequence();
            seq.Append(text.transform.DOScale(1.3f, 0.15f).SetEase(Ease.OutBack));
            seq.Append(text.transform.DOScale(1f, 0.1f));

            Tween pulseScale = text.transform.DOScale(0.8f, 0.15f).SetLoops(-1, LoopType.Yoyo);
            Tween pulseAlpha = text.DOFade(0.4f, 0.1f).SetLoops(-1, LoopType.Yoyo);

            Tween counter = DOTween.To(() => current, x => {
                current = x;
                text.text = FormatValue(current);
            }, to, duration).SetEase(Ease.Linear);

            seq.Join(counter);
            seq.OnComplete(() => {
                pulseScale.Kill();
                pulseAlpha.Kill();
                text.color = normalColor;
                text.transform.DOScale(1f, 0.15f).SetEase(Ease.InBack);
                text.DOFade(1f, 0.15f);
            });
            seq.Play();
        }

        public static void AnimateIncrease(this TextMeshProUGUI text, float from, float to, float duration)
        {
            float current = from;
            text.text = FormatValue(current);

            Sequence seq = DOTween.Sequence();
            seq.Append(text.transform.DOScale(1.3f, 0.15f).SetEase(Ease.OutBack));
            seq.Append(text.transform.DOScale(1f, 0.1f));

            Tween pulseScale = text.transform.DOScale(0.8f, 0.15f).SetLoops(-1, LoopType.Yoyo);
            Tween pulseAlpha = text.DOFade(0.4f, 0.1f).SetLoops(-1, LoopType.Yoyo);

            Tween counter = DOTween.To(() => current, x => {
                current = x;
                text.text = FormatValue(current);
            }, to, duration).SetEase(Ease.Linear);

            seq.Join(counter);
            seq.OnComplete(() => {
                pulseScale.Kill();
                pulseAlpha.Kill();
                text.transform.DOScale(1f, 0.15f).SetEase(Ease.InBack);
                text.DOFade(1f, 0.15f);
            });
            seq.Play();
        }

        public static void AnimateQuotaText(this TextMeshProUGUI text, float from, float to, float max, float duration, string separator = " / ")
        {
            float current = from;
            text.text = $"{current.ToString("N0", CultureInfo.InvariantCulture)}${separator}{max.ToString("N0", CultureInfo.InvariantCulture)}$";

            Sequence seq = DOTween.Sequence();
            seq.Append(text.transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutBack));
            seq.Append(text.transform.DOScale(1f, 0.1f));

            Tween pulseScale = text.transform.DOScale(0.95f, 0.05f).SetLoops(-1, LoopType.Yoyo);
            Tween pulseAlpha = text.DOFade(0.4f, 0.1f).SetLoops(-1, LoopType.Yoyo);

            Tween counter = DOTween.To(() => current, x => {
                current = x;
                text.text = $"{current.ToString("N0", CultureInfo.InvariantCulture)}${separator}{max.ToString("N0", CultureInfo.InvariantCulture)}$";
            }, to, duration).SetEase(Ease.Linear);

            seq.Join(counter);
            seq.OnComplete(() => {
                pulseScale.Kill();
                pulseAlpha.Kill();
                text.transform.DOScale(1f, 0.1f).SetEase(Ease.InBack);
                text.DOFade(1f, 0.1f);
            });
            seq.Play();
        }

        public static void AnimateTimerShake(this TextMeshProUGUI text)
        {
            text.transform.DOShakePosition(0.3f, 5f, 10, 90, false, true).OnComplete(() => text.transform.localPosition = Vector3.zero);
        }
    }
}