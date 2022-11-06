using System.Collections.Generic;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    public List<ApplianceSO> startingAppliances = new List<ApplianceSO>();
    private List<Appliance> _appliances = new List<Appliance>();

    protected override void Awake()
    {
        base.Awake();
        AddStartingAppliance();
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

}
