using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shop", menuName = "Shop/BuyItem")]
public class BuyItem : ScriptableObject
{
    public SlotClass outputItem;




    public void BuyShopItem(InventoryManager inventory)
    {
        inventory.Add(outputItem.GetItem(), outputItem.GetQuantity());

    }
}
