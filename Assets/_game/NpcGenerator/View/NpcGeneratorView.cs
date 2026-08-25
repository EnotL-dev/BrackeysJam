using UnityEngine;

namespace Assets._game.NpcGenerator.View
{
    public class NpcGeneratorView : MonoBehaviour
    {
        private const float HAT_OFFSET_Y = -.05f;

        [SerializeField] private GameObject[] _characterPrefabs;
        [SerializeField] private GameObject[] _hatsPrefabs;


        private void Start()
        {
            for (var i = 0; i < 10; i++)
            {
                GenerateCharacter(new Vector3(0, 0, i * .5f));
            }
        }


        public GameObject GenerateCharacter(Vector3 pos)
        {
            var characterPrefab = _characterPrefabs[Random.Range(0, _characterPrefabs.Length)];
            var characterHandle = new GameObject
            {
                name = $"Character {Random.Range(1, 1000)}"
            };
            characterHandle.transform.position = pos;
            var character = Instantiate(characterPrefab, characterHandle.transform);
            var renderer = character.GetComponent<Renderer>();
            var height = renderer.bounds.size.y;

            var hatPrefab = _hatsPrefabs[Random.Range(0, _hatsPrefabs.Length)];
            var hat = Instantiate(hatPrefab, characterHandle.transform);
            hat.transform.localPosition = new Vector3(0, height + HAT_OFFSET_Y, 0);

            print(height);
            return character;
        }

    }
}
