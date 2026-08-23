using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCFactory : MonoBehaviour {

        [SerializeField] GameObject prefab;


        public GameObject SpawnNpc() {
            return Instantiate(prefab);
        }



    }
}