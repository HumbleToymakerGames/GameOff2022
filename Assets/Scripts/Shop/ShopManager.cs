using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ApplianceSO startingAppliance;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startingAppliance != null) {
            _appliances.Add(new Appliance(startingAppliance));
        }
    }

}
