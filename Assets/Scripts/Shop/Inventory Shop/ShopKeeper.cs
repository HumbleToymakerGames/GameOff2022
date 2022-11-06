using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private ShopItemList shopItemsHeld;
    private InventoryShopSystem shopSystem;

    private void Awake()
    {
        shopSystem = new InventoryShopSystem(shopItemsHeld.Items.Count);

        foreach(var item in shopItemsHeld.Items)
        {
            //shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }
}
