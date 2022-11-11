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
    public UI_ItemQuantityElement functionOutput;
    public Button startfunctionButton;

    public GameObject itemQuantityPrefab;

    public List<SlotClass> inputs = new List<SlotClass>();

    public void SetToApplianceFunction(ApplianceFunction applianceFunction)
    {
        functionNameText.text = applianceFunction.GetApplianceFunctionName();
        functionDurationText.text = applianceFunction.GetDurationString();
        List<SlotClass> inputs = applianceFunction.GetItemQuantitiesForInputs();
        foreach(SlotClass input in inputs)
        {
            GameObject go = Instantiate(itemQuantityPrefab, functionInputParent.transform);
            go.GetComponent<UI_ItemQuantityElement>().SetToItemQuantity(input);
        }
        SlotClass output = applianceFunction.GetItemQuantityForOutput();
        functionOutput.SetToItemQuantity(output);
    }

    public Button GetStartFunctionButton()
    {
        return startfunctionButton;
    }
}
