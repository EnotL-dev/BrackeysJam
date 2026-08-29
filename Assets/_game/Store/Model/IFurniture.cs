using System.Collections;
using UnityEngine;

namespace Assets._game.Store.Model
{
    public interface IFurniture
    {
        bool WasRemoved { get; }
        bool CanBuy();
        FurnitureSO ThisFurnitureSO();
    }
}