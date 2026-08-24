using Assets._game.Interaction.View;
using Assets._game.Npc.View;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCInteractionScript : MonoBehaviour, IInteractable {

        [SerializeField] string dialoge; //TODO use a sperate script for this

        NPCScript NPCScript;
        NPCInfoView NPCInfoView;

        [Inject]
        void Construct(NPCInfoView NPCInfoView) {
            this.NPCInfoView = NPCInfoView;
        }


        public void Start() {
            NPCScript = GetComponent<NPCScript>();
        }

        public string GetTip() => "E to talk";


        public void OnInteract() {

            Debug.Log(dialoge);

            NPCInfoView.Show(NPCScript);



        }


        public bool FreezePlayer() {
            //Stop the npc from moving\

            return true; //for now
        }

        public void OnContinuousInteraction() {
            //throw new System.NotImplementedException();
        }

        public void OnEndInteraction() {
            //throw new System.NotImplementedException();
        }

        public void OnStartInteraction() {
            //throw new System.NotImplementedException();
        }
    }
}