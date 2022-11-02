using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplianceContextActionUI : MonoBehaviour
{
    public TextMeshProUGUI applianceNameText;
    public SimpleButton button;

    private Appliance _currentAppliance;

    public void InitializeWithAppliance(Appliance appliance)
    {
        gameObject.SetActive(false);
        _currentAppliance = appliance;
        applianceNameText.text = _currentAppliance.GetApplianceName();

        /*
        foreach(Appliance appliance in appliance.applianceSO)
        {
            SimpleButton newButton = Instantiate(applianceButtonPrefab, transform);
            newButton.SetButtonText(appliance.GetApplianceName());
            newButton.button.onClick.AddListener(() => UIManager.Instance.ShowApplianceContextPanel(appliance, transform.position));
        }
        */
    }
}
