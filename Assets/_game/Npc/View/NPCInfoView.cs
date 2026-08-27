using Assets._game.Bar.Model.Alcohol;
using Assets._game.Npc.Controller;
using Assets._game.Npc.Enum;
using TMPro;
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

        [SerializeField] private TextMeshProUGUI wealthText;
        [SerializeField] private TextMeshProUGUI characteristicText;
        [SerializeField] private TextMeshProUGUI favoriteDrinkText;



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


        public void Show( NPCScript script ) {

            Debug.Log("open npc info");

            lastNpc = script;

            NPCInfo info = script.npcInfo;

            if ( info == null ) return;

            gameObject.SetActive(true);

            if ( nameText != null ) nameText.text = $"name: {info.name}";
            if ( ageText != null ) ageText.text = $"Age: {info.age}";
            if ( sexText != null ) sexText.text = $"Sex: {info.sex}";
            if ( heightText != null ) heightText.text = $"Height: {info.height:F2} m";
            if ( weightText != null ) weightText.text = $"Weight: {info.weight:F1} kg";

            if ( wealthText != null ) {
                wealthText.text = $"{info.wealth}";

                wealthText.color = info.wealth switch {
                    NPCWealthType.poor => Color.gray,
                    NPCWealthType.normal => Color.green,
                    NPCWealthType.rich => Color.yellow,
                    _ => Color.white
                };
            }

            if ( characteristicText != null ) {
                characteristicText.text = $"{info.npcProperties}";
            }

            if ( favoriteDrinkText != null ) {
                favoriteDrinkText.text = $"{info.farDrink}";

                favoriteDrinkText.color = info.farDrink switch {
                    AlcoholType.Beer => new Color(1f, 0.75f, 0.2f),
                    AlcoholType.Vine => new Color(0.7f, 0.1f, 0.2f),
                    AlcoholType.Vodka => Color.red,
                    _ => Color.white
                };


            }
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