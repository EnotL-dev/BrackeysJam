using Assets._game.TestingScript;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCManager : MonoBehaviour {

        WaitingLineService waitingLineService;
        [SerializeField] NPCFactory NPCFactory;

        [SerializeField] private float minSpawnInterval = 1f;
        [SerializeField] private float maxSpawnInterval = 3f;

        private float spawnTimer;
        private float nextSpawnTime;

        [Inject]
        void Construct( [Inject(Id = "ComeIn")] WaitingLineService waitingLineService ) {
            this.waitingLineService = waitingLineService;
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
            if ( !waitingLineService.HasAvailableSlot() ) return;

            Vector3 pos = waitingLineService.GetNextAvailablePosition();

            var gameObject = NPCFactory.SpawnNpc();
            var npc = gameObject.GetComponent<NPCScript>();

            npc.MoveToDest(pos);

            // For now, just spawn it.
            // Later:
            // waitingLine.AddNPC(npc);
        }






    }
}