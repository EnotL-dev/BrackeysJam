using TMPro;
using UnityEngine;

public class FontUpdaterView : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset _font;


    private void Start()
    {
        var texts = FindObjectsByType<TextMeshProUGUI>();
        print($"update texts {texts}");
        foreach (var text in texts)
        {
            text.font = _font;
        }
    }
}
