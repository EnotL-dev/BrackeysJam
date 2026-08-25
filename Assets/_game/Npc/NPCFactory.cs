using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCFactory : MonoBehaviour {

        [SerializeField] GameObject prefab;
        [SerializeField] private Transform spawnParent;

        [Inject]
        private DiContainer container;

        public GameObject SpawnNpc() {
            return container.InstantiatePrefab(prefab, this.gameObject.transform.position, Quaternion.identity, spawnParent);
        }


    }
}