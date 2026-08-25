using Assets._game.Core.StateMachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableShiftSwitcherView : MonoBehaviour, IInteractable
    {
        [Inject] private SignalBus signalBus;
        [Inject] private IGameStateMachine gameStateMachine;

        [SerializeField] private Transform signObject;

        public bool FreezePlayer() => false;
        public bool IsDragingObject() => false;

        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            //nothing
        }

        public void OnInteract()
        {
            if(lastState is DayShiftState)
                gameStateMachine.Enter<NightShiftState>();
            else
                gameStateMachine.Enter<DayShiftState>();

            FlipSign();
        }

        public void OnStartInteraction()
        {
            //nothing
        }

        private void FlipSign()
        {
            DOTween.Kill(signObject);
            signObject.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutBounce);
        }

        private void OnEnable()
        {
            signalBus.Subscribe<StateChangedSignal>(StateChanged);
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<StateChangedSignal>(StateChanged);
        }

        IGameState lastState; // FOR DEBUG
        public void StateChanged(StateChangedSignal stateChangedSignal)
        {
            if (stateChangedSignal.gameState is DayShiftState)
                print("day");
            else if (stateChangedSignal.gameState is NightShiftState)
                print("night");

            lastState = stateChangedSignal.gameState;
        }
    }
}