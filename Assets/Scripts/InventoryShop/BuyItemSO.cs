using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shop", menuName = "Shop/BuyItem")]
public class BuyItemSO : ScriptableObject
{
    //public List<SlotClass> addItems = new List<SlotClass>();
    public SlotClass addItem;
    public SlotClass subtractItem;
    public int itemPrice;
    




    public void AddItemToCart(ShoppingManager shopFunction)
    {
        shopFunction.Add(addItem.GetItem(), addItem.GetQuantity());
    }

    public void RemoveItemFromCart(ShoppingManager shopFunction)
    {
        shopFunction.Remove(subtractItem.GetItem(), subtractItem.GetQuantity());
    }

    private void AddItemPrice()
    {

    }

    private void SubtractItemPrice()
    {

    }
}
