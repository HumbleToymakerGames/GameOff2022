using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventorySidePanel : MonoBehaviour
{
    public GameObject inventoryItemQuantityElementPrefab;
    public GameObject itemListParent;
    public GameObject visibleSidebar;

    private bool _isSidebarVisible;

    private void OnEnable()
    {
        EventHandler.InventoryDidChangeEvent += ConfigureInventoryPanel;
    }

    private void OnDisable()
    {
        EventHandler.InventoryDidChangeEvent -= ConfigureInventoryPanel;
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
        ConfigureInventoryPanel();
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

    public void ConfigureInventoryPanel()
    {
        foreach(Transform el in itemListParent.transform)
        {
            Destroy(el.gameObject);
        }

        List<SlotClass> items = InventoryManager.Instance.GetInventory();
        foreach(SlotClass item in items)
        {
            GameObject go = Instantiate(inventoryItemQuantityElementPrefab, itemListParent.transform);
            UI_InventoryItemQuantityElement el = go.GetComponent<UI_InventoryItemQuantityElement>();
            el.ConfigureForItem(item);
        }
    }
}
