using Assets._game.Hint.Model;
using UnityEngine;

namespace Assets._game.Hint.Controller
{
    public interface IHintService
    {
        void AddHint(HintType hintType);
        void RemoveHint(HintType hintType);
    }
}