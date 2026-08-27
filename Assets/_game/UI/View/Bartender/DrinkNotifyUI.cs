using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.Alcohol;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets._game.UI.View.Bartender {
    public class DrinkNotifyUI : MonoBehaviour {

        [Header("UI")]
        [SerializeField] private TMP_Text notifyText;
        [SerializeField] private float moveDistance = 15f;
        [SerializeField] private float cycleDuration = 0.6f;

        [Header("Update Interval")]
        [SerializeField] private float checkInterval = 0.5f;

        private IBarService _barService;
        private Tween _floatTween;
        private Vector2 _originalAnchoredPosition;
        private RectTransform _rectTransform;

        [Inject]
        public void Construct( IBarService barService ) {
            _barService = barService;
        }

        private void Awake() {
            if ( notifyText != null ) {
                _rectTransform = notifyText.rectTransform;
                _originalAnchoredPosition = _rectTransform.anchoredPosition;
                notifyText.gameObject.SetActive(false);
            }
        }



        private float _timer;

        private void Update() {
            _timer += Time.deltaTime;
            if ( _timer >= checkInterval ) {
                _timer = 0f;
                CheckStockAndNotify();
            }
        }

        /// <summary>
        /// Call this on Start or whenever bar stock updates.
        /// </summary>
        public void CheckStockAndNotify() {
            if ( _barService == null || notifyText == null ) return;

            Dictionary<AlcoholType, int> stock = _barService.GetAlcoholDictionary();

            if ( IsOutOfStock(stock) ) {
                ShowNotification();
            }
            else {
                HideNotification();
            }
        }

        private void ShowNotification() {
            if ( notifyText.gameObject.activeSelf ) return;

            notifyText.gameObject.SetActive(true);
            _rectTransform.anchoredPosition = _originalAnchoredPosition;

            // Kill any previous tween before creating a new one
            _floatTween?.Kill();

            // Bob up and down indefinitely
            _floatTween = _rectTransform.DOAnchorPosY(_originalAnchoredPosition.y + moveDistance, cycleDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(notifyText.gameObject); // Automatically kills tween if UI is destroyed
        }

        private void HideNotification() {
            _floatTween?.Kill();
            _rectTransform.anchoredPosition = _originalAnchoredPosition;
            notifyText.gameObject.SetActive(false);
        }

        private void OnDestroy() {
            _floatTween?.Kill();
        }

        private bool IsOutOfStock( Dictionary<AlcoholType, int> stock ) {
            if ( stock == null || stock.Count == 0 ) {
                return true;
            }

            foreach ( KeyValuePair<AlcoholType, int> kvp in stock ) {
                //Debug.Log($"stock {kvp.Key} is {kvp.Value}");

                if ( kvp.Value > 0 ) return false;

            }

            // All drink counts are <= 0
            return true;
        }
    }
}