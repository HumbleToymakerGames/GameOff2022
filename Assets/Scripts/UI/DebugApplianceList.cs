using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugApplianceList : MonoBehaviour
{
    public ApplianceButton applianceButton;

    // Start is called before the first frame update
    void Start()
    {
        InitializeListOfButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeListOfButtons()
    {
        applianceButton.applianceName.text = ShopManager.Instance.GetAppliancesInShop()[0].GetApplianceName();
    }
}
