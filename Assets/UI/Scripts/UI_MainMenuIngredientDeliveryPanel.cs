using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenuIngredientDeliveryPanel : MonoBehaviour, I_UIMainMenuPanel
{
    public GameObject ingredientListParent;
    public GameObject ingredientListPrefab;

    private List<UI_ItemSupplyListItem> _itemSupplyListItems = new();

    public void Display()
    {
        Initialize();
    }

    private void Initialize()
    {
        ClearApplianceContext();
        List<ItemClass> knownItems = ShopManager.Instance.GenerateKnownIngredientsList();
        if (knownItems == null || knownItems.Count == 0) return;

        foreach(ItemClass item in knownItems)
        {
            GameObject newEl = Instantiate(ingredientListPrefab, ingredientListParent.transform);
            UI_ItemSupplyListItem newListItemScript = newEl.GetComponent<UI_ItemSupplyListItem>();
            _itemSupplyListItems.Add(newListItemScript);
            newListItemScript.ConfigureForIngredient(item);
        }
    }

    private void ClearApplianceContext()
    {
        foreach (Transform child in ingredientListParent.transform)
        {
            Destroy(child.gameObject);
        }
        _itemSupplyListItems.Clear();
    }

    public void RecaclulateSubtotal()
    {
        // based on your selections so far, recalculate the subtotal
        // needs to be called from the ingredientorderprefab
        // also add an array of classes to this class so that we can foreach over those and get the data out of them
    }
}
