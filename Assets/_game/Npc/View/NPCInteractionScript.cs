using Assets._game.Interaction.View;
using Assets._game.Sound.EnumInterface;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.View {
    public class NPCInteractionScript : MonoBehaviour, IInteractable {

        [SerializeField] string dialoge; //TODO use a sperate script for this

        NPCScript NPCScript;
        NPCInfoView NPCInfoView;

        ISFXService sFXService;

        bool canInteracThisFrame = true;
        [SerializeField] private bool isDraggingObject = false;

        public bool CanInteractThisFrame => canInteracThisFrame;

        public bool IsDraggableObject() => isDraggingObject;
        [Inject]
        void Construct( NPCInfoView NPCInfoView,
            ISFXService sFXService ) {
            this.NPCInfoView = NPCInfoView;
            this.sFXService = sFXService;
        }


        public void Start() {
            NPCScript = GetComponent<NPCScript>();

            if ( NPCScript == null ) Debug.Log("NPCScipt inNPCInteractionScript is null ");
        }

        public string GetTip() => "E to talk";


        public void OnInteract() {

            Debug.Log(dialoge);

            NPCInfoView.Show(NPCScript);

            sFXService.Play(SFXType.NPCSpeech);

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

        public void ModifyCanInteract() => canInteracThisFrame = false;
    }
}