using Assets._game.Npc.Controller;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.Npc.View {
    public class NPCInfoView : MonoBehaviour {

        NPCScript lastNpc;

        [Inject] private NPCService npcService;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI ageText;
        [SerializeField] private TextMeshProUGUI sexText;
        [SerializeField] private TextMeshProUGUI heightText;
        [SerializeField] private TextMeshProUGUI weightText;

        [SerializeField] Button acceptBtn;
        [SerializeField] Button cancelBtn;

        private void OnEnable() {
            acceptBtn?.onClick.AddListener(OnAccecpt);
            cancelBtn?.onClick.AddListener(OnReject);
        }

        private void OnDisable() {
            acceptBtn?.onClick.RemoveAllListeners();
            cancelBtn?.onClick.RemoveAllListeners();
        }


        public void Show( NPCScript script) {

            lastNpc = script;

            NPCInfo info = script.npcInfo;

            if ( info == null ) return;

            nameText.text = $"name: {info.name}";
            ageText.text = $"Age: {info.age}";
            sexText.text = $"Sex: {info.sex}";
            heightText.text = $"Height: {info.height:F2} m";
            weightText.text = $"Weight: {info.weight:F1} kg";

            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }


        void OnAccecpt() {
            // change state
            // set destination for npc

            if ( lastNpc == null )
                return;

            npcService.AcceptNpc(lastNpc);
            Hide();

        }
        
        void OnReject() {
            // change state
            // set destination for npc

            if ( lastNpc == null )
                return;

            npcService.RejectNpc(lastNpc);
            Hide();

        }


    }
}