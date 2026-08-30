using Assets._game.Bar.Controller;
using Assets._game.Bar.Model;
using Assets._game.Sound.Controller;
using Assets._game.Sound.EnumInterface;
using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.View {
    public class BartenderScript : MonoBehaviour {

        IBarService barService;
        ISFXService sfxService;

        Animator animator;

        private static int PouringHash = Animator.StringToHash("IsPourIng");

        private readonly Queue<BartenderOrder> orders = new();

        private bool isPreparing;
        private EventInstance pouringInstance;
        [Inject]
        void Construct( IBarService barService,
            ISFXService sFXService ) {
            this.barService = barService;
            this.sfxService = sFXService;
        }

        private void Start() {
            animator = GetComponent<Animator>();

            barService.OnNpcRequestBar += EnqueueOrder;
        }


        private void OnDestroy() {
            barService.OnNpcRequestBar -= EnqueueOrder;
        }


        private void EnqueueOrder( float prepareTime, Action onComplete ) {
            orders.Enqueue(new BartenderOrder(prepareTime, onComplete));

            TryProcessNext();
        }


        private void TryProcessNext() {
            if ( isPreparing ) return;

            if ( orders.Count == 0 ) return;

            StartCoroutine(ProcessNextOrder());
        }


        private IEnumerator ProcessNextOrder() {
            isPreparing = true;

            BartenderOrder order = orders.Dequeue();

            StartPouring();

            yield return new WaitForSeconds(order.prepareTime);

            StopPouring();

            order.onComplete?.Invoke();

            isPreparing = false;

            TryProcessNext();
        }

        private void StartPouring() {
            animator.SetBool(PouringHash, true);

            pouringInstance = sfxService.StartLoop(SFXType.BartenderPourBeer, gameObject);
        }

        private void StopPouring() {
            animator.SetBool(PouringHash, false);

            if ( pouringInstance.isValid() ) {
                pouringInstance.stop(STOP_MODE.ALLOWFADEOUT);

                pouringInstance.release();
                pouringInstance.clearHandle();
            }
        }
    }

    class BartenderOrder {
        public float prepareTime;
        public Action onComplete;

        public BartenderOrder(
            float prepareTime,
            Action onComplete ) {
            this.prepareTime = prepareTime;
            this.onComplete = onComplete;
        }
    }

}

