using Assets._game.Core.StateMachine;
using Assets._game.Sound.Controller;
using Assets._game.Sound.EnumInterface;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.SpaceFighter;

namespace Assets._game.Core.View
{
    public class EndGameStarter : MonoBehaviour
    {
        [Inject] SignalBus signalBus;

        [SerializeField] private PlayerController playerController;
        [SerializeField] private Camera playerCamera;
        [Space(5)]
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private ParticleSystem particlesExplosion;
        [SerializeField] private GameObject starterObject;

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
            if (stateChangedSignal.gameState is EndGameState)
            {
                playerController.FreezeMovement();
                playerCamera.gameObject.SetActive(false);
                StartCoroutine(StartEndGame());
            }
        }

        IEnumerator StartEndGame()
        {
            starterObject.SetActive(true);

            yield return new WaitForSeconds(3);

            explosion.Play();
            particlesExplosion.Play();

            yield return new WaitForSeconds(0.2f);

            SceneManager.LoadScene("TavernScene");

            yield return null;
        }
    }
}