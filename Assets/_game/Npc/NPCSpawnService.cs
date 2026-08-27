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
        NPCInfoGenerator npcInfoGenerator = new();

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

            if ( npcInfoGenerator == null ) {
                Debug.Log("somwhow infogenerator is null");
                npcInfoGenerator = new NPCInfoGenerator();
            }

            NPCInfo info = npcInfoGenerator.Generate();

            if ( info == null ) {
                Debug.Log("can't get info");
                return;
            }

            npc.Initialize(info);

            npc.MoveToDest(pos);

            // For now, just spawn it.
            // Later:
            // waitingLine.AddNPC(npc);
        }






    }
}