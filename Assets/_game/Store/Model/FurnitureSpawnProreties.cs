using System.Collections;
using UnityEngine;

namespace Assets._game.Store.Model
{
    [System.Serializable]
    public class FurnitureSpawnProreties
    {
        public GameObject spawnPrefab;
        [Space(5)]
        public Transform spawnPoint;
    }
}