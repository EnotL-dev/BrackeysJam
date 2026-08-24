using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class NPCFactory : MonoBehaviour {

        [SerializeField] GameObject prefab;

        [Inject]
        private DiContainer container;

        public GameObject SpawnNpc() {
            return container.InstantiatePrefab(prefab);
        }


    }
}