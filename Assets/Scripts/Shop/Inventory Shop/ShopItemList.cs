using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop System/Shop Item List")]

public class ShopItemList : ScriptableObject
{
    [SerializeField] private List<ShopInventoryItem> items;
    

    public List<ShopInventoryItem> Items => items;
}

[System.Serializable]
public struct ShopInventoryItem
{
    public Item itemData;
    public int amount;
}
