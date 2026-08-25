using UnityEngine;

namespace Assets._game.NpcGenerator.View
{
    public class NpcGeneratorView : MonoBehaviour
    {

        [SerializeField] private NpcGeneratedCharacterView[] _characters;
        [SerializeField] private GameObject[] _hats;
        [SerializeField] private GameObject[] _mustaches;
        [SerializeField] private GameObject[] _beards;


        private void Start()
        {
            for (var i = 0; i < 20; i++)
            {
                GenerateCharacter(new Vector3(0, 0, i * .75f));
            }
        }


        public NpcGeneratedCharacterView GenerateCharacter(Vector3 pos)
        {
            var characterPrefab = _characters[Random.Range(0, _characters.Length)];
            var hatModel = Random.Range(0, 2) == 0 ? new GameObject()
                : _hats[Random.Range(0, _hats.Length)];
            var mustacheModel = Random.Range(0, 2) == 0 ? new GameObject()
                : _mustaches[Random.Range(0, _mustaches.Length)];
            var beardModel = Random.Range(0, 2) == 0 ? new GameObject()
                : _beards[Random.Range(0, _beards.Length)];

            var character = Instantiate(characterPrefab);
            character.name = $"Character {Random.Range(1, 1000)}";
            character.transform.position = pos;
            character.AddAceesories(hat: hatModel, beard: beardModel, mustache: mustacheModel);
            return character;
        }
    }
}
