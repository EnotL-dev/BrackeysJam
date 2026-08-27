using Assets._game.Core.StateMachine;
using Assets._game.Player.View;
using Assets._game.Shift.Controller;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableShiftSwitcherView : MonoBehaviour, IInteractable
    {
        [Inject] SignalBus signalBus;
        [Inject] IShiftService shiftService;

        [SerializeField] private Transform signObject;

        public string GetTip() => canSwitch ? "[E] - change the shift to night" : "It's already the night shift";
        public bool FreezePlayer() => false;
        public bool IsDraggableObject() => false;

        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            //nothing
        }

        public bool OnceActivation() => true;

        bool canSwitch = true;
        public void OnInteract()
        {
            if (!canSwitch) return;

            shiftService.StartNightShift();
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

        public void StateChanged(StateChangedSignal stateChangedSignal)
        {
            if (stateChangedSignal.gameState is DayShiftState)
            {
                canSwitch = true;
                FlipSign();

                Debug.Log("<color=magenta>Day shift</color>");
            }
            else if (stateChangedSignal.gameState is NightShiftState)
            {
                canSwitch = false;
                FlipSign();

                Debug.Log("<color=magenta>Night shift</color>");
            }
        }
    }
}