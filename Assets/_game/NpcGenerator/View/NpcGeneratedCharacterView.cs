using UnityEngine;

namespace Assets._game.NpcGenerator.View
{

    public class NpcGeneratedCharacterView : MonoBehaviour
    {
        [SerializeField] private GameObject _hatHandle;
        [SerializeField] private GameObject _beardHandle;
        [SerializeField] private GameObject _mustacheHandle;


        public void AddAceesories(GameObject hat, GameObject beard, GameObject mustache)
        {
            Instantiate(hat, _hatHandle.transform);
            Instantiate(beard, _beardHandle.transform);
            Instantiate(mustache, _mustacheHandle.transform);
        }
    }
}
