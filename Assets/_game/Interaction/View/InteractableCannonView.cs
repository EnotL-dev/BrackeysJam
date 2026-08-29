using Assets._game.Store.Model;
using Assets._game.Store.View;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableCannonView : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform cannonObject;
        [SerializeField] private CanonLoader canonLoader;
        [SerializeField] private Transform spawnShootPoint;
        [Space(5)]
        [SerializeField] private ParticleSystem particleSmoke;

        private float initialScaleX;

        private Transform visitorObject = null;
        public void LoadVisitor(Transform visitorObject)
        {
            visitorObject.gameObject.SetActive(false);
            this.visitorObject = visitorObject;
        }

        void Start()
        {
            initialScaleX = cannonObject.localScale.x;
        }

        public string GetTip()
        {
            return "[E] - SHOOT!";
        }

        public bool OnceActivation() => true;
        public bool FreezePlayer() => false;
        public bool IsDraggableObject() => false;
        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {

        }

        public void OnInteract()
        {
            Shoot();
            canonLoader.UnLoadCanon();
        }

        private void Shoot()
        {
            float targetScaleX = initialScaleX * 0.75f;
            float shrinkDuration = 0.2f;
            float returnDuration = 0.1f;

            cannonObject.DOKill();

            Sequence sequence = DOTween.Sequence();

            sequence.Append(cannonObject.DOScaleX(targetScaleX, shrinkDuration)
                                .SetEase(Ease.OutQuad));

            sequence.Append(cannonObject.DOScaleX(initialScaleX, returnDuration)
                                .SetEase(Ease.InQuad));

            sequence.Append(cannonObject.DOShakeRotation(0.8f, 8, 15)
                                .SetEase(Ease.OutQuad));

            sequence.Play();

            Transform tempVisitorObject = visitorObject;
            tempVisitorObject.transform.position = spawnShootPoint.position;
            tempVisitorObject.transform.rotation = spawnShootPoint.rotation;
            tempVisitorObject.gameObject.SetActive(true);
            tempVisitorObject.GetComponent<Rigidbody>().AddForce(tempVisitorObject.up * 30f, ForceMode.Impulse);
            tempVisitorObject.DOScaleY(1.3f, 3f).SetEase(Ease.InBack).OnComplete(() => Destroy(tempVisitorObject.gameObject));

            particleSmoke.Stop();
            particleSmoke.Play();

            visitorObject = null;
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}