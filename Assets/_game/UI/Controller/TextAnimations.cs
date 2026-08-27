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
    }
}