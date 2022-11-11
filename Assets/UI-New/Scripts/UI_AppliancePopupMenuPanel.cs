using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_AppliancePopupMenuPanel : MonoBehaviour
{
    public GameObject applianceFunctionParent;
    public GameObject applianceFunctionListItemPrefab;
    private TextMeshProUGUI _applianceNameText;

    public void Start()
    {
        _applianceNameText = GameObject.Find("HeaderText").GetComponent<TextMeshProUGUI>();
    }

    public void InitializeWithAppliance(Appliance appliance)
    {
        _applianceNameText.text = appliance.GetApplianceName();
        List<ApplianceFunction> functions = appliance.GetApplianceFunctions();
        foreach (ApplianceFunction function in functions)
        {
            GameObject newFunctionElement = Instantiate(applianceFunctionListItemPrefab, applianceFunctionParent.transform);
            UI_ApplianceFunctionListItemElement newFunctionScript = newFunctionElement.GetComponent<UI_ApplianceFunctionListItemElement>();
            newFunctionScript.ConfigureForApplianceFunction(function);
            // Configure button?
        }
    }

    public void ClearApplianceContext()
    {
        _applianceNameText.text = "Appliance Not Set";
        foreach(Transform child in applianceFunctionParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
