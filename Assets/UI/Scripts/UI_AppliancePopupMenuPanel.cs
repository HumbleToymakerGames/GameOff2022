using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_AppliancePopupMenuPanel : MonoBehaviour
{
    public GameObject applianceFunctionParent;
    public GameObject applianceFunctionListItemPrefab;
    public Button startApplianceFunctionButton;
    public TextMeshProUGUI insufficientItemsText;
    private TextMeshProUGUI _applianceNameText;

    public void Start()
    {
        _applianceNameText = GameObject.Find("HeaderText").GetComponent<TextMeshProUGUI>();
    }

    public void InitializeWithAppliance(Appliance appliance)
    {
        ClearApplianceContext();
        _applianceNameText.text = appliance.GetApplianceName();
        List<ApplianceFunction> functions = appliance.GetApplianceFunctions();
        foreach (ApplianceFunction function in functions)
        {
            GameObject newFunctionElement = Instantiate(applianceFunctionListItemPrefab, applianceFunctionParent.transform);
            UI_ApplianceFunctionListItemElement newFunctionScript = newFunctionElement.GetComponent<UI_ApplianceFunctionListItemElement>();
            newFunctionScript.ConfigureForApplianceFunction(function);
        }
        startApplianceFunctionButton.gameObject.SetActive(false);
    }

    public void ClearApplianceContext()
    {
        _applianceNameText.text = "Appliance Not Set";
        foreach(Transform child in applianceFunctionParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void PreviewApplianceFunction(ApplianceFunction function)
    {
       if (function.CanAffordFunction())
        {
            startApplianceFunctionButton.gameObject.SetActive(true);
            startApplianceFunctionButton.onClick.AddListener(() => function.parentAppliance.FunctionStarted(function));
            startApplianceFunctionButton.GetComponentInChildren<TextMeshProUGUI>().text = function.GetApplianceFunctionName();
            insufficientItemsText.gameObject.SetActive(false);
        } else
        {
            startApplianceFunctionButton.gameObject.SetActive(false);
            insufficientItemsText.gameObject.SetActive(true);
        }
    }
}
