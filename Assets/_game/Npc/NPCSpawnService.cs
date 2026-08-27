using Assets._game.Npc.Controller;
using Assets._game.Npc.View;
using Assets._game.TestingScript;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCSpawnService : MonoBehaviour {

        WaitingLineService waitingLineService;
        [SerializeField] NPCFactory NPCFactory;
        NPCInfoGenerator npcInfoGenerator;

        [SerializeField] private float minSpawnInterval = 1f;
        [SerializeField] private float maxSpawnInterval = 3f;

        private float spawnTimer;
        private float nextSpawnTime;

        [Inject]
        void Construct( [Inject(Id = "ComeIn")] WaitingLineService waitingLineService,
            NPCInfoGenerator npcInfoGenerator ) {
            this.waitingLineService = waitingLineService;
            this.npcInfoGenerator = npcInfoGenerator;
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

                yield return new WaitForSeconds(delay);

                SpawnNPC();
            }
        }


        //TODO: chagne into a service to provide the position in waitingline
        //to the npc script
        private void SpawnNPC() {

            if ( !waitingLineService.TryReserve(out Vector3 targetPos) ) {
                return;
            }

            NPCInfo info = npcInfoGenerator.Generate();
            if ( info == null ) {
                Debug.Log("Can't get info");
                // Revert the reservation if NPC creation fails
                waitingLineService.CancelReservation();
                return;
            }

            var gameObject = NPCFactory.SpawnNpc();
            var npc = gameObject.GetComponent<NPCScript>();

            npc.Initialize(info);
            npc.MoveToDest(targetPos);

            // For now, just spawn it.
            // Later:
            // waitingLine.AddNPC(npc);
        }






    }
}