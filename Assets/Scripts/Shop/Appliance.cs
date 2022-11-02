using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance
{
    private ApplianceSO _applianceSO;

    public Appliance(ApplianceSO applianceSO)
    {
        _applianceSO = applianceSO;
    }

    public string GetApplianceName()
    {
        return _applianceSO.applianceName;
    }
    
    /*
    public ApplianceFunctionSO[] GetApplianceFunctions()
    {
        return _applianceSO.functions;
    }
    */
}
