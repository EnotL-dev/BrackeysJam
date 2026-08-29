using UnityEngine;

namespace Assets._game.Hint.Model
{
    [CreateAssetMenu(fileName = "Hint", menuName = "Add Hint/Hint")]
    public class HintSO : ScriptableObject
    {
        [SerializeField] private string _title;
        public string Title() => _title;
        [SerializeField] private HintType _hintType;
        public HintType HintType => _hintType;
    }
}