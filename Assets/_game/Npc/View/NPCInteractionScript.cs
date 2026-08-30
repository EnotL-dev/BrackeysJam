using Assets._game.Interaction.View;
using Assets._game.Sound.EnumInterface;
using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.View {
    public class NPCInteractionScript : MonoBehaviour, IInteractable {

        [SerializeField] string dialoge; //TODO use a sperate script for this
        [Space(5)]
        [SerializeField] private Outline outlineObject;
        public void ShowOutline()
        {
            if (outlineObject)
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

        NPCScript npcScript;
        NPCInfoView NPCInfoView;
        ISFXService sFXService;

        SkinnedMeshRenderer[] renderers;
        Animator animator;


        int countBeforeKnockOut = 5;
        int currentCount = 0;

        Rigidbody rd;
        Collider collider;


        static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");


        bool canInteractThisFrame = true;
        private bool isDraggingObject = false;
        bool isDameableObject = true;
        bool isKnockOut = false;
        bool isAttacked = false;
        public bool CanInteractThisFrame() => canInteractThisFrame;

        public bool IsDraggableObject() => isDraggingObject;
        public bool IsDameableObject() => isDameableObject;
        public bool ShowCursor() => isKnockOut ? false : true;
        [Inject]
        void Construct( NPCInfoView NPCInfoView,
            ISFXService sFXService ) {
            this.NPCInfoView = NPCInfoView;
            this.sFXService = sFXService;
        }


        public void Start() {
            npcScript = GetComponent<NPCScript>();
            renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

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
                PlayHurtEffect();
                isAttacked = true;
                currentCount++;
                sFXService.Play(SFXType.Hit);
            }


            if ( currentCount > countBeforeKnockOut ) {
                rd ??= this.gameObject.GetComponent<Rigidbody>();
                collider ??= gameObject.GetComponent<Collider>();


                rd.isKinematic = false;
                rd.useGravity = false;

                collider.isTrigger = true;

                sFXService.Play(SFXType.KnockOut);

                isKnockOut = true;
                npcScript.StopAllBehaviour();
                LeanBack();

                StartCoroutine(RecoverFromKnockOut(() => {
                    isKnockOut = false;
                    rd.isKinematic = true;
                    rd.useGravity = true;
                    collider.isTrigger = false;
                    animator.enabled = true;
                    npcScript.RecoverFromKnockOut();
                }));
            }
        }

        public void LeanBack() {
            // Tweens local rotation on the X axis to -90 degrees
            animator ??= npcScript.animator;
            animator.enabled = false;

            isDraggingObject = true;

            transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 0.5f).SetEase(Ease.OutQuad);
        }

        public void PlayHurtEffect() {
            Color red = Color.red;
            Sequence sequence = DOTween.Sequence();
            Vector3 originalScale = transform.localScale;

            var tcs = new TaskCompletionSource<bool>();

            foreach ( Renderer renderer in renderers ) {
                Material mat = renderer.material;
                mat.SetColor(EmissionColor, Color.black);

                sequence.Join(
                    mat.DOColor(red * 3f, EmissionColor, 0.15f)
                );
            }

            // Stay glowing.
            sequence.AppendInterval(0.1f);

            // Scale up.
            sequence.Append(
                transform.DOScale(originalScale * 1.15f, 0.15f).SetEase(Ease.OutBack)
            );

            // Scale back down.
            sequence.Append(
                transform.DOScale(originalScale, 0.15f).SetEase(Ease.InOutSine)
            );

            // Remove glow from all renderers.
            foreach ( Renderer renderer in renderers ) {
                Material mat = renderer.material;

                sequence.Join(
                    mat.DOColor(Color.black, EmissionColor, 0.3f)
                );
            }

            sequence.OnComplete(() => {
                tcs.SetResult(true);
            });
        }

        private IEnumerator RecoverFromKnockOut( Action onComplete ) {
            // Wait for the knock-out pose to finish.
            yield return new WaitForSeconds(30f);

            // Rotate NPC back upright.
            yield return transform
                .DOLocalRotate(Vector3.zero, 0.5f)
                .SetEase(Ease.OutQuad)
                .WaitForCompletion();

            onComplete?.Invoke();
        }
    }
}