using Assets._game.UI.Controller;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._game.Bar.View
{
    public class DeskManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI vibeText;
        [SerializeField] private TextMeshProUGUI quotaText;
        [SerializeField] private TextMeshProUGUI shiftText;

        public void UpdateShiftText(int count)
        {
            shiftText.text = $"{count}";
        }

        public void UpdateVibe(int count)
        {
            vibeText.text = $"{count}";
        }

        public void UpdateQuotaText(int balance, int quota)
        {
            quotaText.text = $"{balance} $ / {quota} $";
        }
        
    }
}