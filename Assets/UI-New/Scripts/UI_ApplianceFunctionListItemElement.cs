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

    public Button startFunctionButton;

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

        foreach (Sprite sprite in GetInputItemsSprites())
        {
            GameObject inputItemEl = Instantiate(singleInputItemPrefab, inputItemsParent.transform);
            inputItemEl.GetComponent<UI_InventoryItemSingleElement>().SetSprite(sprite);
        }

        if (applianceFunction.CanAffordFunction())
        {
            ConfigureUIForCanUse();
        } else
        {
            ConfigureUIForCannotUse();
        }
    }

    public List<Sprite> GetInputItemsSprites()
    {
        List<Sprite> spriteList = new List<Sprite>();
        List<SlotClass> inputItems = _applianceFunction.GetItemQuantitiesForInputs();
        foreach(SlotClass inputItem in inputItems)
        {
            for(int i = 0; i < inputItem.GetQuantity(); i++)
            {
                spriteList.Add(inputItem.GetItem().itemIcon);
            }
        }
        return spriteList;
    }

    private void ConfigureUIForCanUse()
    {
        rootImage.GetComponent<Image>().color = Color.white;
        startFunctionButton.onClick.AddListener(() => _applianceFunction.parentAppliance.FunctionClicked(_applianceFunction));
    }

    private void ConfigureUIForCannotUse()
    {
        rootImage.GetComponent<Image>().color = Color.red;
    }
}