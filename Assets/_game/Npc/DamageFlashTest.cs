using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._game.Npc {
    public class DamageFlashTest : MonoBehaviour {

        [Header("Punch Scale Settings")]
        [SerializeField] private Vector3 punchScale = new Vector3(0.15f, 0.15f, 0.15f);
        [SerializeField] private float punchDuration = 0.25f;
        [SerializeField] private int punchVibrato = 5;
        [SerializeField] private float punchElasticity = 0.5f;

        [Header("Damage Flash Settings")]
        [SerializeField, ColorUsage(true, true)]
        private Color flashColor = Color.red * 3f; // HDR color multiplier for brighter emission
        [SerializeField] private float flashDuration = 0.15f;
        [SerializeField] private float emissionDuration = 0.2f;

        [Header("Debug / Testing")]
        [SerializeField] private InputActionReference testKey;
        [SerializeField] bool playEffect;

        private SkinnedMeshRenderer[] skinnedMeshRenderers;
        private List<Material> cachedMaterials = new List<Material>();
        private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
        private Coroutine flashCoroutine;

        private Sequence damageSequence;
        private Vector3 initialScale;

        private void OnEnable() {
            testKey.action.Enable();

            if ( testKey == null ) {
                Debug.Log("test key is null ");
            }

            if ( testKey.action != null ) {
                Debug.Log("action is null");
            }

            if ( testKey != null && testKey.action != null ) {

                testKey.action.Enable();
            }
            Debug.Log("test key is null or action is null");

            //testKey.action.performed += TriggerDamageFlash;
        }

        private void OnDisable() {
            if ( testKey != null && testKey.action != null ) {
                testKey.action.Disable();
            }
        }

        //private void Awake() {
        //    initialScale = transform.localScale;
        //    FindAndDebugRenderers();
        //}

        private void Update() {
            //if ( testKey.action.WasPressedThisFrame() ) {
            //    Debug.Log("This should call for emission");
            //    TriggerDamageFlash();
            //}


            if ( playEffect ) {
                TriggerDamageFlash();
            }
        }

        [ContextMenu("Find & Debug Skinned Mesh Renderers")]
        public void FindAndDebugRenderers() {

            // 1. Find all SkinnedMeshRenderer components in children (including inactive)
            skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
            cachedMaterials.Clear();

            Debug.Log($"<color=cyan>[DamageFlashTest]</color> Found {skinnedMeshRenderers.Length} SkinnedMeshRenderer(s) under '{gameObject.name}':");

            foreach ( var smr in skinnedMeshRenderers ) {
                Debug.Log($"- GameObject: <b>{smr.gameObject.name}</b> | Material count: {smr.materials.Length}");

                // 2. Cache materials and enable the shader's emission keyword
                foreach ( var mat in smr.materials ) {
                    if ( mat.HasProperty(EmissionColorID) ) {
                        mat.EnableKeyword("_EMISSION");
                        cachedMaterials.Add(mat);
                    }
                }
            }
        }

        [ContextMenu("Test Damage Flash")]
        public void TriggerDamageFlash() {

            Debug.Log("This shuold call for dame");

            damageSequence?.Kill(true);
            transform.localScale = initialScale;

            damageSequence = DOTween.Sequence();

            // 1. Punch scale animation
            damageSequence.Join(transform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity));

            // 2. Emission flash and fade back to black
            foreach ( var mat in cachedMaterials ) {
                if ( mat == null ) continue;

                mat.SetColor(EmissionColorID, flashColor);
                damageSequence.Join(
                    mat.DOColor(Color.black, EmissionColorID, emissionDuration)
                       .SetEase(Ease.OutQuad)
                );
            }

            damageSequence.OnComplete(() => { playEffect = false; });
        }

        private IEnumerator DamageFlashRoutine() {
            SetEmission(flashColor);

            yield return new WaitForSeconds(flashDuration);

            SetEmission(Color.black);
            flashCoroutine = null;
        }

        private void SetEmission( Color color ) {
            foreach ( var mat in cachedMaterials ) {
                if ( mat != null ) {
                    mat.SetColor(EmissionColorID, color);
                }
            }
        }
    }
}