using UnityEngine;

namespace Assets._game.NpcGenerator.View
{
    public class NpcGeneratorView : MonoBehaviour
    {
        private static int _counter;

        [SerializeField] private NpcGeneratedCharacterView[] _characters;
        [SerializeField] private GameObject[] _hats;
        [SerializeField] private GameObject[] _mustaches;
        [SerializeField] private GameObject[] _beards;
        [SerializeField] private GameObject[] _hairs;
        [SerializeField] private GameObject[] _ears;
        [SerializeField] private GameObject[] _horns;


        // private void Start()
        // {
        //     for (var i = 0; i < 20; i++)
        //     {
        //         GenerateCharacter(new Vector3(0, 0, i * .75f));
        //     }
        // }

        public NpcGeneratedCharacterView GenerateCharacter(Vector3 pos)
        {
            var characterPrefab = _characters[Random.Range(0, _characters.Length)];
            var hatModel = Random.Range(0, 2) == 0 ? null
                : _hats[Random.Range(0, _hats.Length)];
            var mustacheModel = Random.Range(0, 2) == 0 ? null
                : _mustaches[Random.Range(0, _mustaches.Length)];
            var beardModel = Random.Range(0, 2) == 0 ? null
                : _beards[Random.Range(0, _beards.Length)];
            var hairModel = Random.Range(0, 5) == 0 ? null
            : _hairs[Random.Range(0, _hairs.Length)];
            var earsModel = _ears[Random.Range(0, _ears.Length)];
            var hornsModel = _horns[Random.Range(0, _horns.Length)];


            var character = Instantiate(characterPrefab);
            character.name = $"Character {_counter++}";
            character.transform.position = pos;
            character.AddAceesories(
                hat: hatModel,
                beard: beardModel,
                mustache: mustacheModel,
                hair: hairModel,
                ears: earsModel,
                horns: hornsModel
            );
            return character;
        }
    }
}
