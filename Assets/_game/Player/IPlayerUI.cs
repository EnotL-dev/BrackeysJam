using System.Collections;
using UnityEngine;

namespace Assets._game.Player {
    public interface IPlayerUI {
        bool IsOpen { get; }
        void Open();
        void Close();
    }
}