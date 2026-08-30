using Assets._game.Npc.View;
using Assets._game.NpcGenerator.View;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCFactory : MonoBehaviour {

        [SerializeField] private GameObject _visitorBrain;
        [SerializeField] private NpcGeneratorView _npcGenerator;
        [SerializeField] private Transform spawnParent;


        private DiContainer container;
        private WorldSettingScript worldSettingScript;

        [Inject]
        void Construct( DiContainer diContainer,
            WorldSettingScript worldSettingScript ) {
            this.container = diContainer;
            this.worldSettingScript = worldSettingScript;

        }

        public GameObject SpawnNpc() {
            var visitor = container.InstantiatePrefab(_visitorBrain, worldSettingScript.GetSpawnPoint(), Quaternion.identity, spawnParent);
            var character = _npcGenerator.GenerateCharacter(Vector3.zero);

            character.transform.SetParent(visitor.transform);
            character.transform.localPosition = Vector3.zero;

            var npc = visitor.GetComponent<NPCScript>();
            npc.SetCharacterModel(character);

            return visitor;
        }


    }
}