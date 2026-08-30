using Assets._game.UI.Controller;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._game.Bar.View
{
    public class DeskManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI vibeText;
        [SerializeField] private TextMeshProUGUI quotaText;
        [SerializeField] private TextMeshProUGUI shiftText;
        [SerializeField] private Slider chaosSlider;
        [SerializeField] private TextMeshProUGUI chaosText;

        public void UpdateShiftText(int count)
        {
            shiftText.text = $"{count}";
            UpdateChaosScale(0);
        }

        public void UpdateVibe(int count)
        {
            vibeText.text = $"{count}";
        }

        public void UpdateQuotaText(int balance, int quota)
        {
            quotaText.text = $"{balance} $ / {quota} $";
        }

        public void UpdateChaosScale(float scale)
        {
            chaosSlider.value = scale;
            if(scale <= 0.01)
                chaosSlider.fillRect.gameObject.SetActive(false);
            else
                chaosSlider.fillRect.gameObject.SetActive(true);

            chaosText.text = ((int)(scale * 100)).ToString();
        }
        
    }
}