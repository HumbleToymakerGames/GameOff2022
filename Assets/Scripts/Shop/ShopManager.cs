using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    public List<ApplianceSO> startingAppliances = new();
    public List<ApplianceFunctionSO> startingRecipes = new();
    private List<Appliance> _appliances = new List<Appliance>();
    private List<ApplianceFunctionSO> _knownRecipes = new();
    private List<ItemClass> _knownIngredients = new();
    private List<SlotClass> _supplyOrder = new();

    public int shopStartingMoney = 100;
    private int _shopMoney = 0;

    protected override void Awake()
    {
        base.Awake();
        AddStartingAppliance();
    }

    private void Start()
    {
        AddMoney(shopStartingMoney);
        foreach(ApplianceFunctionSO recipe in startingRecipes)
        {
            LearnRecipe(recipe);
        }
    }

    private void OnEnable()
    {
        EventHandler.IngredientSupplyDeliveryWasMadeEvent += OnInventorySupplyDeliveryWasMade;
    }

    private void OnDisable()
    {
        EventHandler.IngredientSupplyDeliveryWasMadeEvent -= OnInventorySupplyDeliveryWasMade;
    }

    public void LearnRecipe(ApplianceFunctionSO recipe)
    {
        _knownRecipes.Add(recipe);
        _knownIngredients = GenerateKnownIngredientsList();
        EventHandler.CallDidLearnRecipeEvent(recipe);
    }

    public List<ApplianceFunctionSO> GetKnownRecipes()
    {
        return _knownRecipes;
    }

    private void AddStartingAppliance()
    {
        if (startingAppliances.Count > 0) {
            foreach(ApplianceSO startingAppliance in startingAppliances)
            {
                _appliances.Add(new Appliance(startingAppliance));
            }
        }
    }

    // Only ingredients that are used in known recipes should be purchaseable.
    // Return all unique ingredients which appear in known recipes.
    // TODO: Don't return ingredients which are intermediate products (ex: dough) ?
    public List<ItemClass> GenerateKnownIngredientsList()
    {
        List<ItemClass> knownIngredients = new();
        foreach(ApplianceFunctionSO recipe in _knownRecipes)
        {
            foreach(SlotClass ingredient in recipe.inputItems)
            {
                if (knownIngredients.Contains(ingredient.GetItem())) break;
                knownIngredients.Add(ingredient.GetItem());
            }
        }
        return knownIngredients;
    }

    public List<Appliance> GetAppliancesInShop()
    {
        return _appliances;
    }

    public bool AddMoney(int moneyToEarn)
    {
        _shopMoney += moneyToEarn;
        EventHandler.CallShopMoneyDidChangeEvent(moneyToEarn, GetCurrentShopMoney());
        return true;
    }

    public int GetCurrentShopMoney()
    {
        return _shopMoney;
    }

    // Get the last set quantity desired for a particular ingredient
    // in the supply order.
    public int GetCurrentOrderForIngredient(ItemClass ingredient)
    {
        foreach(SlotClass slot in _supplyOrder)
        {
            if (slot.GetItem() == ingredient) return slot.GetQuantity();
        }
        return 0;
    }

    public void UpdateRecurringOrder(SlotClass updatedOrder)
    {
        foreach(SlotClass slot in _supplyOrder)
        {
            if (slot.GetItem() == updatedOrder.GetItem())
            {
                slot.SetQuantity(updatedOrder.GetQuantity());
                return;
            }
        }
        _supplyOrder.Add(updatedOrder);
    }

    private void OnInventorySupplyDeliveryWasMade()
    {
        int pendingTotal = 0;
        foreach(SlotClass slot in _supplyOrder)
        {
            pendingTotal += slot.GetQuantity() * slot.GetItem().baseCost;
        }
        if (pendingTotal > GetCurrentShopMoney())
        {
            Debug.Log("Could not make purchase because of insufficient funds!");
            return;
        }

        foreach(SlotClass slot in _supplyOrder)
        {
            InventoryManager.Instance.Add(slot.GetItem(), slot.GetQuantity());
        }
        AddMoney(-pendingTotal);
    }
}
