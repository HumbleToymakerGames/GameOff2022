using System.Collections.Generic;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    public List<ApplianceSO> startingAppliances = new List<ApplianceSO>();
    private List<Appliance> _appliances = new List<Appliance>();

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
