using Assets._game.Store.View;
using System.Collections;
using UnityEngine;

namespace Assets._game.Store.Model
{
    [System.Serializable]
    public class FurniturePlaces 
    {
        public FurnitureType furnitureType = FurnitureType.chair;
        [Space(5)]
        public bool Busy = false; // if this place is already busy (have object)
        public FurnitureGhostPlace placeGhost;
        public GameObject realObject;
    }
}