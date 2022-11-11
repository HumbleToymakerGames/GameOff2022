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
    public GameObject inputItemsParent;
    public GameObject singleInputItemPrefab;

    public Button startFunctionButton;

    public void ConfigureForApplianceFunction(ApplianceFunction applianceFunction)
    {
        SlotClass outputItem = applianceFunction.GetItemQuantityForOutput();
        applianceFunctionNameText.text = applianceFunction.GetApplianceFunctionName();
        applianceFunctionDurationText.text = applianceFunction.GetDurationString();
        outputProduct.GetComponent<UI_ItemQuantityElement>().SetToItemQuantity(outputItem);
        startFunctionButton.onClick.AddListener(() => applianceFunction.parentAppliance.FunctionClicked(applianceFunction));
        foreach(Transform el in inputItemsParent.transform)
        {
            Destroy(el.gameObject);
        }

        foreach (Sprite sprite in GetInputItemsSprites(applianceFunction))
        {
            GameObject inputItemEl = Instantiate(singleInputItemPrefab, inputItemsParent.transform);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetSprite(sprite);
        } 
    }

    public List<Sprite> GetInputItemsSprites(ApplianceFunction applianceFunction)
    {
        List<Sprite> spriteList = new List<Sprite>();
        List<SlotClass> inputItems = applianceFunction.GetItemQuantitiesForInputs();
        foreach(SlotClass inputItem in inputItems)
        {
            for(int i = 0; i < inputItem.GetQuantity(); i++)
            {
                spriteList.Add(inputItem.GetItem().itemIcon);
            }
        }
        return spriteList;
    }
}
