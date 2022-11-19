using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenuRecipePanel : MonoBehaviour, I_UIMainMenuPanel
{
    public GameObject applianceFunctionParent;
    public GameObject applianceFunctionListItemPrefab;

    public void Display()
    {
        InitializeWithKnownRecipes();
    }

    private void InitializeWithKnownRecipes()
    {
        ClearApplianceContext();
        List<ApplianceFunctionSO> knownRecipes = ShopManager.Instance.GetKnownRecipes();
        if (knownRecipes == null) return;
        
        foreach (ApplianceFunctionSO recipe in knownRecipes)
        {
            GameObject newFunctionElement = Instantiate(applianceFunctionListItemPrefab, applianceFunctionParent.transform);
            UI_ApplianceFunctionListItemElement newFunctionScript = newFunctionElement.GetComponent<UI_ApplianceFunctionListItemElement>();
            newFunctionScript.ConfigureForApplianceFunction(recipe);
        }
    }

    private void ClearApplianceContext()
    {
        foreach (Transform child in applianceFunctionParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
