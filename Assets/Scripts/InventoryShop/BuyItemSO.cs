using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shop", menuName = "Shop/BuyItem")]
public class BuyItemSO : ScriptableObject
{
    //public List<SlotClass> addItems = new List<SlotClass>();
    public SlotClass addItem;
    public SlotClass subtractItem;

    public void AddItemToCart(ShopFunction shopFunction)
    {
        shopFunction.Add(addItem.GetItem(), addItem.GetQuantity());
    }

    public void RemoveItemFromCart(ShopFunction shopFunction)
    {
        shopFunction.Remove(subtractItem.GetItem(), subtractItem.GetQuantity());
    }
}
