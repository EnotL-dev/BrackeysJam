using UnityEngine;

namespace Assets._game.Store.Model
{
    public class FurnitureSO : ScriptableObject
    {
        [SerializeField] private string _name = "node";
        public string Name() => _name;
        [SerializeField] private int _cost = 10;
        public int Cost() => _cost;
        [SerializeField] private string _description = "none";
        public string Description() => _description;
        [SerializeField] private FurnitureType furnitureType = FurnitureType.chair;
        public FurnitureType GetFurnitureType() => furnitureType;
    }
}
