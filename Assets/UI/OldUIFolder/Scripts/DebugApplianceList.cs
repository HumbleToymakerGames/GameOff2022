using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugApplianceList : MonoBehaviour
{
    public SimpleButton applianceButtonPrefab;

    void Start()
    {
        InitializeListOfButtons();
    }

    void InitializeListOfButtons()
    {
        List<Appliance> appliances = ShopManager.Instance.GetAppliancesInShop();
        foreach(Appliance appliance in appliances)
        {
            SimpleButton newButton = Instantiate(applianceButtonPrefab, transform);
            newButton.SetButtonText(appliance.GetApplianceName());
            // newButton.button.onClick.AddListener(() => UIManager.Instance.ShowApplianceContextPanel(appliance, transform.position));
        }
    }
}
