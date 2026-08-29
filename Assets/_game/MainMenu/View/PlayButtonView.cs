using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _lights;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _lights.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _lights.SetActive(false);
    }
}
