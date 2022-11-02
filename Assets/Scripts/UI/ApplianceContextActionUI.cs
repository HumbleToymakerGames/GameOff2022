using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplianceContextActionUI : MonoBehaviour
{
    public TextMeshProUGUI applianceNameText;
    public SimpleButton buttonPrefab;

    private Appliance _currentAppliance;

    public void InitializeWithAppliance(Appliance appliance)
    {
        gameObject.SetActive(false);

        foreach(Transform child in transform)
        {
            if (child.gameObject.name != "MenuTitleText") Destroy(child.gameObject);
        }

        _currentAppliance = appliance;
        applianceNameText.text = _currentAppliance.GetApplianceName();

        List<ApplianceFunction> functions = _currentAppliance.GetApplianceFunctions();
        foreach(ApplianceFunction function in functions)
        {
            SimpleButton newButton = Instantiate(buttonPrefab, transform);
            newButton.SetButtonText(function.GetApplianceFunctionName());
        }
        SimpleButton backButton = Instantiate(buttonPrefab, transform);
        backButton.SetButtonText("Back");
        backButton.button.onClick.AddListener(() => UIManager.Instance.CloseApplianceContextPanel());
    }
}
