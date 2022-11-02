using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<ApplianceSO> startingAppliances = new List<ApplianceSO>();
    private List<Appliance> _appliances = new List<Appliance>();

    private static ShopManager _instance;

    public static ShopManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AddStartingAppliance();
    }

    // Start is called before the first frame update
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
