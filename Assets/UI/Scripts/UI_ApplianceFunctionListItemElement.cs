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
    public GameObject rootImage;

    public Button selectFunctionButton;

    private ApplianceFunction _applianceFunction;

    public void ConfigureForApplianceFunction(ApplianceFunction applianceFunction)
    {
        _applianceFunction = applianceFunction;
        SlotClass outputItem = _applianceFunction.GetItemQuantityForOutput();
        applianceFunctionNameText.text = _applianceFunction.GetApplianceFunctionName();
        applianceFunctionDurationText.text = _applianceFunction.GetDurationString();
        outputProduct.GetComponent<UI_ItemQuantityElement>().SetToItemQuantity(outputItem);
        
        foreach(Transform el in inputItemsParent.transform)
        {
            Destroy(el.gameObject);
        }

        foreach (ItemAffordability itemAffordability in GetInputItemsAndAffordability())
        {
            GameObject inputItemEl = Instantiate(singleInputItemPrefab, inputItemsParent.transform);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetSprite(itemAffordability.item.itemIcon);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetDoesHave(itemAffordability.isAffordable);
        }

        selectFunctionButton.onClick.AddListener(() => _applianceFunction.parentAppliance.FunctionClicked(_applianceFunction));
    }

    public void ConfigureForApplianceFunction(ApplianceFunctionSO applianceFunction)
    {
        // overload when initializing recipes menu
        // don't need information about affordability or button functionality
        // TODO: use ApplianceFunction or ApplianceFunctionSO for all?

        SlotClass outputItem = applianceFunction.outputItem;
        applianceFunctionNameText.text = outputItem.GetItem().itemName;
        applianceFunctionDurationText.text = applianceFunction.hoursToMake.ToString();
        outputProduct.GetComponent<UI_ItemQuantityElement>().SetToItemQuantity(outputItem);

        foreach (Transform el in inputItemsParent.transform)
        {
            Destroy(el.gameObject);
        }
        
        foreach(SlotClass ingredient in applianceFunction.inputItems)
        {
            GameObject inputItemEl = Instantiate(singleInputItemPrefab, inputItemsParent.transform);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetSprite(ingredient.GetItem().itemIcon);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetDoesHave(true);
        }

    }

    private List<ItemAffordability> GetInputItemsAndAffordability()
    {
        List<ItemAffordability> returnList = new List<ItemAffordability>();
        List<SlotClass> inputItems = _applianceFunction.GetItemQuantitiesForInputs();
        foreach(SlotClass inputItem in inputItems)
        {
            int itemCount = InventoryManager.Instance.GetCountOfItem(inputItem.GetItem());
            for(int i = 0; i < inputItem.GetQuantity(); i++)
            {
                ItemAffordability returnStruct;
                returnStruct.isAffordable = itemCount > i;
                returnStruct.item = inputItem.GetItem();
                returnList.Add(returnStruct);
            }
        }
        return returnList;
    }
}

public struct ItemAffordability
{
    public ItemClass item;
    public bool isAffordable;
}