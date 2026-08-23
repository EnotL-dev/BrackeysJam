using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCFactory : MonoBehaviour {

        [SerializeField] GameObject prefab;


        public GameObject SpawnNpc() {
            return Instantiate(prefab);
        }



    }
}