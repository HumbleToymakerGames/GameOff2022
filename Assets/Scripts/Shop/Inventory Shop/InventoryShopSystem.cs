using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShopSystem
{
    private List<ShopSlot> shopInventory;

    public InventoryShopSystem(int size)
    {
        SetShopSize(size);
    }

    private void SetShopSize(int size)
    {
        shopInventory = new List<ShopSlot>(size);

        for (int i = 0; i < size; i++)
        {
            shopInventory.Add(item: new ShopSlot());
        }
    }

    public void AddToShop(Item data, int amount)
    {



    }



}
