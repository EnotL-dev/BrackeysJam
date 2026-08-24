using Assets._game.TestingScript;
using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCManager : MonoBehaviour {

        [SerializeField] WaitingLineScript waitingLineScript;
        [SerializeField] NPCFactory NPCFactory;

        [SerializeField] private float minSpawnInterval = 1f;
        [SerializeField] private float maxSpawnInterval = 3f;

        private float spawnTimer;
        private float nextSpawnTime;



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
            if ( !waitingLineScript.HasAvailableSlot() ) return;

            Transform target = waitingLineScript.GetMostPosition();

            var gameObject = NPCFactory.SpawnNpc();
            var npc = gameObject.GetComponent<NPCScript>();

            npc.MoveToWaitingLine(target);

            // For now, just spawn it.
            // Later:
            // waitingLine.AddNPC(npc);
        }






    }
}