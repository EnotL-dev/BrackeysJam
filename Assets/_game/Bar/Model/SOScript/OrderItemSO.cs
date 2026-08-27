using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class OrderItemSO : ScriptableObject{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    [SerializeField] private float _prepareTime;
    [SerializeField] private float consumeTime;
    [SerializeField] private int buyCost;
    [SerializeField] private int soldCost;

    public Sprite Icon => _icon;
    public string Name => _name;
    public float ConsumeTime => consumeTime;
    public float PrepareTime => _prepareTime;
    public int BuyCost => buyCost;
    public int SoldCost => soldCost;

}
