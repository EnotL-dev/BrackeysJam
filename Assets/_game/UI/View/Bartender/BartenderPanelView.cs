using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Bar.Model.Alcohol;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.UI.View
{
    public class BartenderPanelView : MonoBehaviour
    {
        [Inject] DiContainer container;
        [Inject] IEconomyService economyService;

        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private AlchoholPanelView prefabAlcPanel;
        private List<AlchoholPanelView> panels = new List<AlchoholPanelView>();

        private CanvasGroup canvasGroup;

        private AlcoholSO[] alcohols;

        public void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            InitAlchoholData();
        }

        private void InitAlchoholData()
        {
            alcohols = Resources.LoadAll<AlcoholSO>("Bar/Alchohol/DrinkSO/Alcohol");
            Array.Sort(alcohols, (a, b) => a.BuyCost.CompareTo(b.BuyCost));
        }

        public void BuyAlchohol(AlcoholType alcoholType, int count)
        {
            economyService.BuyAlchohol(alcoholType, count);

            foreach (AlchoholPanelView panel in panels)
            {
                panel.UpdateUI();
            }
        }

        public void OpenPanel()
        {
            if (panels.Count < 1) SpawnPanels();

            foreach(AlchoholPanelView panel in panels)
            {
                panel.UpdateUI();
            }

            ShowMyself();
        }

        private void SpawnPanels()
        {
            foreach(AlcoholSO alc in alcohols)
            {
                AlchoholPanelView newpanel = container.InstantiatePrefab(prefabAlcPanel).GetComponent<AlchoholPanelView>();
                newpanel.transform.SetParent(gridLayoutGroup.transform);
                panels.Add(newpanel);

                newpanel.Initialize(alc.Type, BuyAlchohol);
            }
        }

        public void ClosePanel()
        {
            HideMyself();
        }

        private void ShowMyself()
        {
            DOTween.Kill(canvasGroup);
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1f, 0.3f).From(0f).SetEase(Ease.OutBack).OnComplete(() => canvasGroup.interactable = true);
        }

        private void HideMyself()
        {
            DOTween.Kill(canvasGroup.gameObject);
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => canvasGroup.interactable = false);
        }

        private void OnDisable()
        {
            DOTween.Kill(gameObject);
        }
    }
}