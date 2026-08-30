using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Hint.Controller;
using Assets._game.Interaction.View;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Hint.View
{
    public class TutorialShiftSwitcherActivate : MonoBehaviour
    {
        // For activate switcher when Bar is already full
        [Inject] IBarService barService;

        [SerializeField] private InteractableShiftSwitcherView switcherView;

        private void LateUpdate()
        {
            Dictionary<AlcoholType, int> alcs = barService.GetAlcoholDictionary();
            foreach(KeyValuePair<AlcoholType, int> alc in alcs)
            {
                if(alc.Value > 0)
                {
                    gameObject.layer = LayerMask.NameToLayer("Interactable");
                    switcherView.enabled = true;
                    enabled = false;
                }
            }
        }
    }
}
