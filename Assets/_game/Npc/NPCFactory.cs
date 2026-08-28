using System.Collections;
using Assets._game.NpcGenerator.View;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc
{
    public class NPCFactory : MonoBehaviour
    {

        [SerializeField] private GameObject _visitorBrain;
        [SerializeField] private NpcGeneratorView _npcGenerator;
        [SerializeField] private Transform spawnParent;

        [Inject]
        private DiContainer container;

        public GameObject SpawnNpc()
        {
            var visitor = container.InstantiatePrefab(_visitorBrain, this.gameObject.transform.position, Quaternion.identity, spawnParent);
            var character = _npcGenerator.GenerateCharacter(Vector3.zero);
            character.transform.SetParent(visitor.transform);
            character.transform.localPosition = Vector3.zero;
            return visitor;
        }


    }
}