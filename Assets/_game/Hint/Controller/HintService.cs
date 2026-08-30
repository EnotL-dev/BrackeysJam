using Assets._game.Bar.Controller;
using Assets._game.Hint.Model;
using Assets._game.Player.View;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets._game.Hint.Controller
{
    public class HintService : IHintService, IInitializable
    {
        [Inject] ISeatService seatService;
        [Inject] PlayerInterfaceManagerView playerInterfaceManagerView;

        List<HintSO> AllHints;
        List<HintSO> activeHints;

        public void Initialize()
        {
            activeHints = new List<HintSO>();
            AllHints = Resources.LoadAll<HintSO>("Hint").ToList();
            seatService.OnSeatCountChanged += NotifyWhenSeatChanged;

            HintSO hint_NoAlcohol = AllHints.ToArray().FirstOrDefault(x => x.HintType == HintType.NoAlcohol);
            HintSO hint_CompleteFirstQuota = AllHints.ToArray().FirstOrDefault(x => x.HintType == HintType.CompleteFirstQouta);
            playerInterfaceManagerView.AddHint(hint_NoAlcohol);
            playerInterfaceManagerView.AddHint(hint_CompleteFirstQuota);
        }

        public void AddHint(HintType hintType)
        {
            foreach (HintSO hint in AllHints)
            {
                if(hint.HintType == hintType)
                {
                    activeHints.Add(hint);
                    playerInterfaceManagerView.AddHint(hint);
                    break;
                }
            }
        }

        public void RemoveHint(HintType hintType)
        {
            foreach (HintSO hint in AllHints)
            {
                if (hint.HintType == hintType)
                {
                    activeHints.Remove(hint);
                    playerInterfaceManagerView.RemoveHint(hint.HintType);
                    break;
                }
            }
        }

        public void NotifyWhenSeatChanged(int current, int max)
        {
            if (max < 1)
                AddHint(HintType.NoSeats);
            else
                RemoveHint(HintType.NoSeats);
        }
    }
}