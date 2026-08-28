using Assets._game.Interaction.View;
using Assets._game.Sound.EnumInterface;
using DG.Tweening;
using System.Diagnostics.Metrics;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.View {
    public class NPCInteractionScript : MonoBehaviour, IInteractable {

        [SerializeField] string dialoge; //TODO use a sperate script for this

        NPCScript npcScript;
        NPCInfoView NPCInfoView;

        ISFXService sFXService;

        int countBeforeKnockOut = 5;
        int currentCount = 0;

        bool canInteractThisFrame = true;
        private bool isDraggingObject = false;
        bool isDameableObject = true;
        bool isKnockOut = false;
        bool isAttacked = false;
        public bool CanInteractThisFrame() => canInteractThisFrame;

        public bool IsDraggableObject() => isDraggingObject;
        public bool IsDameableObject() => isDameableObject;
        public bool ShowCursor() => true;
        [Inject]
        void Construct( NPCInfoView NPCInfoView,
            ISFXService sFXService ) {
            this.NPCInfoView = NPCInfoView;
            this.sFXService = sFXService;
        }


        public void Start() {
            npcScript = GetComponent<NPCScript>();

            if ( npcScript == null ) Debug.Log("NPCScipt inNPCInteractionScript is null ");
        }

        public string GetTip() {
            if ( isKnockOut ) return "E to drag";

            if ( isAttacked ) return null;

            return "E to talk";
        }


        public void OnInteract() {
            if ( isKnockOut || isAttacked ) return;

            Debug.Log(dialoge);

            NPCInfoView.Show(npcScript);

            sFXService.Play(SFXType.NPCSpeech);

            isDameableObject = false;
        }


        public bool FreezePlayer() {
            //Stop the npc from moving\
            if ( isKnockOut ) return false;


            return true; //for now
        }




        public void OnContinuousInteraction() {
            //throw new System.NotImplementedException();
        }

        public void OnEndInteraction() {
            NPCInfoView.Hide();
            isDameableObject = true;
        }

        public void OnStartInteraction() {

        }

        public void ModifyCanInteract() => canInteractThisFrame = false;

        public void TryAttack() {
            if ( !canInteractThisFrame ) return;

            if ( isDameableObject ) {
                isAttacked = true;
                currentCount++;
                sFXService.Play(SFXType.Hit);
            }


            if ( currentCount > countBeforeKnockOut ) {
                var rd = this.gameObject.GetComponent<Rigidbody>();
                rd.isKinematic = false;

                sFXService.Play(SFXType.KnockOut);
                npcScript.StopAllBehaviour();
                isKnockOut = true;
                LeanBack();
            }
        }

        public void LeanBack() {
            // Tweens local rotation on the X axis to -90 degrees
            npcScript.animator.enabled = false;

            isDraggingObject = true;

            transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 0.5f).SetEase(Ease.OutQuad);
        }
    }
}