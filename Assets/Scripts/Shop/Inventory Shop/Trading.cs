using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    Store store;

    ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;

    private void Awake()
    {
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
    }


    public void BeginTrading(Store store)
    {
        this.store = store;
        
        itemStorePanel.SetInventory(store.storeContent);

        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);

        
    }

    internal void BuyItem(int id)
    {
        Item itemToBuy = store.storeContent.slots[id].item;
        playerInventory.AddItemToInventory(itemToBuy);

    }
}
