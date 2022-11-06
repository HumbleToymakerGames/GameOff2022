using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop System/Shop Item List")]

public class ShopItemList : ScriptableObject
{
    [SerializeField] private List<ShopInventoryItem> item;
    [SerializeField] private int maxAllowedGold;
    [SerializeField] private float sellMarkUp;
    [SerializeField] private float buyMarkUp;


}

[System.Serializable]
public struct ShopInventoryItem
{
    public Item itemData;
    public int amount;
}
