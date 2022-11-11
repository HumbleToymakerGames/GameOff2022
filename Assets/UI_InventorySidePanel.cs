using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventorySidePanel : MonoBehaviour
{
    private GameObject _visibleSidebar;
    private bool _isSidebarVisible;

    private void Start()
    {
        _visibleSidebar = GameObject.Find("VisibleSidebar");
        HideSidebar();
    }

    public void HideSidebar()
    {
        _visibleSidebar.SetActive(false);
        _isSidebarVisible = false;
    }

    public void ShowSidebar()
    {
        _visibleSidebar.SetActive(true);
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
}
