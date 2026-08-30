using Assets._game.Hint.Model;
using TMPro;
using UnityEngine;

namespace Assets._game.UI.View
{
    public class HintPanel : MonoBehaviour
    {
        [HideInInspector] public HintType hintType;
        [SerializeField] private TextMeshProUGUI titleText;
        
        public void Initialize(string message, HintType hintType)
        {
            this.hintType = hintType;
            titleText.text = message;
        }
    }
}