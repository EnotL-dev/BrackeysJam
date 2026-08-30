using Assets._game.Core.StateMachine;
using Assets._game.Npc.Controller;
using Assets._game.Npc.View;
using Assets._game.TestingScript;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCSpawnService : MonoBehaviour {

        WaitingLineService waitingLineService;
        SignalBus signalBus;

        [SerializeField] NPCFactory NPCFactory;
        NPCInfoGenerator npcInfoGenerator;

        [SerializeField] private float minSpawnInterval = 1f;
        [SerializeField] private float maxSpawnInterval = 3f;

        private float spawnTimer;
        private float nextSpawnTime;
        bool IsNight = false;

        [Inject]
        void Construct( [Inject(Id = "ComeIn")] WaitingLineService waitingLineService,
            NPCInfoGenerator npcInfoGenerator,
            SignalBus signalBus ) {
            this.waitingLineService = waitingLineService;
            this.npcInfoGenerator = npcInfoGenerator;
            this.signalBus = signalBus;
        }


        private void Start() {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine() {
            while ( true ) {
                float delay = Random.Range(
                minSpawnInterval,
                maxSpawnInterval
            );

                while ( !IsNight ) {
                    yield return new WaitForSeconds(1);
                }

                yield return new WaitForSeconds(delay);

                SpawnNPC();
            }
        }


        //TODO: chagne into a service to provide the position in waitingline
        //to the npc script
        private void SpawnNPC() {

            var gameObject = NPCFactory.SpawnNpc();
            var npc = gameObject.GetComponent<NPCScript>();


            if ( !waitingLineService.TryReserve(npc, out Vector3 targetPos) ) {
                Destroy(gameObject);
                return;
            }

            NPCInfo info = npcInfoGenerator.Generate();
            if ( info == null ) {
                Debug.Log("Can't get info");
                // Revert the reservation if NPC creation fails
                waitingLineService.CancelReservation(npc);
                return;
            }





            npc.Initialize(info);
            npc.MoveToDest(targetPos, Enum.NPCMovementOwner.Action);

            // For now, just spawn it.
            // Later:
            // waitingLine.AddNPC(npc);
        }

        private void OnEnable() {
            signalBus.Subscribe<StateChangedSignal>(StateChanged);
        }

        private void OnDisable() {
            signalBus.Unsubscribe<StateChangedSignal>(StateChanged);
        }


        public void StateChanged( StateChangedSignal stateChangedSignal ) {
            if ( stateChangedSignal.gameState is DayShiftState ) {
                IsNight = false;
            }
            else if ( stateChangedSignal.gameState is NightShiftState ) {
                IsNight = true;

                Debug.Log("Update night shift in nps spawn service");
            }
        }
    }
}