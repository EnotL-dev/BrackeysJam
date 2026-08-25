using Assets._game.Bar.Model;
using DG.Tweening;
using UnityEngine;

namespace Assets._game.UI.View
{
    public class BartenderPanelView : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private AlcoholSO[] alcohols;

        public void Start()
        {
            InitAlchoholData();
        }

        private void InitAlchoholData()
        {
            alcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol");

            foreach (AlcoholSO alcohol in alcohols)
            {
                Debug.Log($"{alcohol.Type} - {alcohol.BuyCost}");
            }
        }

        public void ShowMyself()
        {
            DOTween.Kill(gameObject);
            canvasGroup.alpha = 0f;
            transform.DOScale(1f, 0.3f).From(0f).SetEase(Ease.OutBack).OnComplete(() => canvasGroup.interactable = true);
        }

        public void HideMyself()
        {
            DOTween.Kill(gameObject);
            canvasGroup.alpha = 1f;
            transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => canvasGroup.interactable = false);
        }

        private void OnDisable()
        {
            DOTween.Kill(gameObject);
        }
    }
}