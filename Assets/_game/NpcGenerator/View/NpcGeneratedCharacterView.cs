using UnityEngine;

namespace Assets._game.NpcGenerator.View
{

    public class NpcGeneratedCharacterView : MonoBehaviour
    {
        [SerializeField] private GameObject _hatHandle;
        [SerializeField] private GameObject _beardHandle;
        [SerializeField] private GameObject _mustacheHandle;
        [SerializeField] private GameObject _hairHandle;


        public void AddAceesories(GameObject hat, GameObject beard, GameObject mustache, GameObject hair)
        {
            if (_hatHandle != null && hat != null)
            {
                var obj = Instantiate(hat, _hatHandle.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (_beardHandle != null && beard != null)
            {
                var obj = Instantiate(beard, _beardHandle.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (_mustacheHandle != null && mustache != null)
            {
                var obj = Instantiate(mustache, _mustacheHandle.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (_hairHandle != null && hair != null)
            {
                var obj = Instantiate(hair, _hairHandle.transform);
                obj.transform.localPosition = Vector3.zero;
            }
        }
    }
}
