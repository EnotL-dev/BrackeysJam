using Assets._game.Npc.View;
using Assets._game.NpcGenerator.View;
using System.Collections;
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

            var npc = visitor.GetComponent<NPCScript>();
            npc.SetCharacterModel(character);

            return visitor;
        }


    }
}