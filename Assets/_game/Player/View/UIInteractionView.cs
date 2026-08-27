using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets._game.Player.View
{
    public class UIInteractionView : MonoBehaviour
    {
        private bool tipShowed = false;
        [SerializeField] private TextMeshProUGUI textTip;


        public void ShowTip(string text)
        {
            if (tipShowed) return;
            tipShowed = true;

            textTip.text = text;

            textTip.alpha = 0;
            textTip.DOKill();
            textTip.DOFade(0.9f, 0.7f).SetEase(Ease.OutQuad);
        }

        public void HideTip()
        {
            if (!tipShowed) return;
            tipShowed = false;

            textTip.alpha = 1;
            textTip.DOKill();
            textTip.DOFade(0f, 0.4f).SetEase(Ease.OutQuad);
        }
    }
}