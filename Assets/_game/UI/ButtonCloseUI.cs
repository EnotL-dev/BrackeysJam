using Assets._game.Player.Controller;
using Assets._game.UI.View;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.UI {
    public class ButtonCloseUI : MonoBehaviour {

        [Inject] private IPlayerInteractionService interactionService;

        [SerializeField] SettingPanel settingPanel;

        private Button btn;

        private void Awake() {
            btn = GetComponent<Button>();
        }

        private void OnEnable() {
            btn?.onClick.AddListener(ClosePanel);
        }

        private void OnDisable() {
            btn?.onClick.RemoveListener(ClosePanel);
        }

        private void ClosePanel() {
            interactionService.CloseCurrentUI();
            settingPanel.gameObject.SetActive(false); //this for now, this should close in interactionService
        }

    }
}