using UnityEngine;


public abstract class OrderItemSO : ScriptableObject{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _cost;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Cost => _cost;

}
