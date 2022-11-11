using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ApplianceFunctionListItemElement : MonoBehaviour
{
    public TextMeshProUGUI applianceFunctionNameText;
    public TextMeshProUGUI applianceFunctionDurationText;
    public GameObject outputProduct;

    public Button startFunctionButton;

    public void ConfigureForApplianceFunction(ApplianceFunction applianceFunction)
    {
        SlotClass outputItem = applianceFunction.GetItemQuantityForOutput();
        applianceFunctionNameText.text = applianceFunction.GetApplianceFunctionName();
        applianceFunctionDurationText.text = applianceFunction.GetDurationString();
        outputProduct.GetComponent<UI_ItemQuantityElement>().SetToItemQuantity(outputItem);
        startFunctionButton.onClick.AddListener(() => applianceFunction.parentAppliance.FunctionClicked(applianceFunction));
    }
}
