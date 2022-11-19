using System.Collections.Generic;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    public List<ApplianceSO> startingAppliances = new();
    public List<ApplianceFunctionSO> startingRecipes = new();
    private List<Appliance> _appliances = new List<Appliance>();
    private List<ApplianceFunctionSO> _knownRecipes = new();
    private List<ItemClass> _knownIngredients = new();

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
    private List<ItemClass> GenerateKnownIngredientsList()
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

}
