using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StoreDisplaySidePanel : MonoBehaviour
{
    public GameObject inventoryItemQuantityElementPrefab;
    public GameObject itemListParent;
    public GameObject visibleSidebar;

    private bool _isSidebarVisible;

    private void OnEnable()
    {
        EventHandler.InventoryDidChangeEvent += ConfigureStoreDisplayPanel;
    }

    private void OnDisable()
    {
        EventHandler.InventoryDidChangeEvent -= ConfigureStoreDisplayPanel;
    }
    private void Start()
    {
        HideSidebar();
    }

    public void HideSidebar()
    {
        visibleSidebar.SetActive(false);
        _isSidebarVisible = false;
    }

    public void ShowSidebar()
    {
        ConfigureStoreDisplayPanel();
        visibleSidebar.SetActive(true);
        _isSidebarVisible = true;
    }

    public void ToggleSidebar()
    {
        if (_isSidebarVisible)
        {
            HideSidebar();
            
        } else
        {
            ShowSidebar();
        }
    }

    public void ConfigureStoreDisplayPanel()
    {
        foreach(Transform el in itemListParent.transform)
        {
            Destroy(el.gameObject);
        }

        List<SlotClass> items = InventoryManager.Instance.GetStoreStock();
        foreach(SlotClass item in items)
        {
            GameObject go = Instantiate(inventoryItemQuantityElementPrefab, itemListParent.transform);
            UI_InventoryItemQuantityElement el = go.GetComponent<UI_InventoryItemQuantityElement>();
            el.ConfigureForItem(item);
        }
    }
}
