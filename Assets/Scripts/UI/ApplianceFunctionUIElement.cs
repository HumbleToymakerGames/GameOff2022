using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ApplianceFunctionUIElement : MonoBehaviour
{
    public TextMeshProUGUI functionNameText;
    public GameObject functionInputParent;
    public TextMeshProUGUI functionDurationText;
    public ItemQuantityUIElement functionOutput;
    public Button startfunctionButton;

    public GameObject itemQuantityPrefab;

    public List<ItemQuantity> inputs = new List<ItemQuantity>();

    public void SetToApplianceFunction(ApplianceFunction applianceFunction)
    {
        functionNameText.text = applianceFunction.GetApplianceFunctionName();
        functionDurationText.text = applianceFunction.GetDurationString();
        List<ItemQuantity> inputs = applianceFunction.GetItemQuantitiesForInputs();
        foreach(ItemQuantity input in inputs)
        {
            GameObject go = Instantiate(itemQuantityPrefab, functionInputParent.transform);
            go.GetComponent<ItemQuantityUIElement>().SetToItemQuantity(input);
        }
        ItemQuantity output = applianceFunction.GetItemQuantityForOutput();
        functionOutput.SetToItemQuantity(output);
    }

    public Button GetStartFunctionButton()
    {
        return startfunctionButton;
    }
}
