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

        foreach (KeyValuePair<Sprite, bool> spriteAffordability in GetInputItemsSpritesAndAffordability())
        {
            GameObject inputItemEl = Instantiate(singleInputItemPrefab, inputItemsParent.transform);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetSprite(spriteAffordability.Key);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetDoesHave(spriteAffordability.Value);
        }

        selectFunctionButton.onClick.AddListener(() => _applianceFunction.parentAppliance.FunctionClicked(_applianceFunction));


    }

    private Dictionary<Sprite, bool> GetInputItemsSpritesAndAffordability()
    {
        Dictionary<Sprite, bool> returnDictionary = new Dictionary<Sprite, bool>();
        List<SlotClass> inputItems = _applianceFunction.GetItemQuantitiesForInputs();
        foreach(SlotClass inputItem in inputItems)
        {
            int itemCount = InventoryManager.Instance.GetCountOfItem(inputItem.GetItem());
            bool hasThisItem;
            for(int i = 0; i < inputItem.GetQuantity(); i++)
            {
                hasThisItem = itemCount > i;
                returnDictionary.Add(inputItem.GetItem().itemIcon, hasThisItem);
            }
        }
        return returnDictionary;
    }
}