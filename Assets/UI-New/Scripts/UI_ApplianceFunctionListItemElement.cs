using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ApplianceFunctionListItemElement : MonoBehaviour
{
    public TextMeshProUGUI applianceFunctionNameText;
    public TextMeshProUGUI applianceFunctionDurationText;
    public GameObject outputProduct;

    public void ConfigureForApplianceFunction(ApplianceFunction applianceFunction)
    {
        ItemQuantity outputItem = applianceFunction.GetItemQuantityForOutput();
        applianceFunctionNameText.text = applianceFunction.GetApplianceFunctionName();
        applianceFunctionDurationText.text = applianceFunction.GetDurationString();
        outputProduct.GetComponent<UI_ItemQuantityElement>().SetToItemQuantity(outputItem);
    }
}
