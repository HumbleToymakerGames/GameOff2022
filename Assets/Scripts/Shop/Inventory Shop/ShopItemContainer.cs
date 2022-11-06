using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopItemContainer : MonoBehaviour
{
    public List<ItemSlot> slots;

    public void AddItemToShopInventory(Item item, int count = 1)
    {
        if (item.stackable == true)
        {
            //add stackable item
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }





        }
        else
        {
            //add nonstackable item to inventory
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }





    }
}
