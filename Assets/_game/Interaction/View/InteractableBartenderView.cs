using Assets._game.Bar.Controller;
using Assets._game.UI.View;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableBartenderView : MonoBehaviour, IInteractable
    {
        [Inject] IEconomyService economyService;

        [SerializeField] private BartenderPanelView bartenderPanelView;
        [SerializeField] private RectTransform parentPanelToAddText;
        [SerializeField] private RectTransform prefabAddMoney;
        [Space(5)]
        [SerializeField] private Outline outlineObject;
        public void ShowOutline()
        {
            if(outlineObject)
            {
                outlineObject.enabled = true;
            }
        }

        public void HideOutline()
        {
            if (outlineObject)
            {
                outlineObject.enabled = false;
            }
        }

        public void AddMoney(int count)
        {
            RectTransform instance = Instantiate(prefabAddMoney, parentPanelToAddText.transform);
            instance.localPosition = Vector3.zero;

            TextMeshProUGUI tmp = instance.GetComponent<TextMeshProUGUI>();
            tmp.text = $"{count} $";

            tmp.DOFade(1, 0.3f).SetEase(Ease.OutQuad);
            instance.DOLocalMoveY(60, 0.2f).SetEase(Ease.OutQuad);
            tmp.DOFade(0, 0.3f).SetDelay(1.2f).OnComplete(() => Destroy(instance.gameObject));
        }

        private void Start()
        {
            economyService.NotifySell += AddMoney;
        }

        public string GetTip() => "[E] - Buy Drinks";

        public bool FreezePlayer() => true;

        public bool IsDraggableObject() => false;

        public bool ShowCursor() => true;

        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            bartenderPanelView.ClosePanel();
        }

        public void OnInteract()
        {
            bartenderPanelView.OpenPanel();
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}