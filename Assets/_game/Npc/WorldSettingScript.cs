using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc {
    public class WorldSettingScript : MonoBehaviour {

        [SerializeField] Vector3 SpawnPoint;
        [SerializeField] Vector3 LeavePoint;


        public Vector3 GetSpawnPoint() => SpawnPoint == null ? new Vector3(30, -0.5f, 25) : SpawnPoint;
        public Vector3 GetLeavePoint() => LeavePoint == null ? new Vector3(30, -0.5f, 25) : LeavePoint;


    }
}