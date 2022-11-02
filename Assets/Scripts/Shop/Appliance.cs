using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance
{
    private ApplianceSO _applianceSO;
    private List<ApplianceFunction> _applianceFunctions = new List<ApplianceFunction>();

    public Appliance(ApplianceSO applianceSO)
    {
        _applianceSO = applianceSO;
        List<ApplianceFunctionSO> applianceFunctionSOs = _applianceSO.functions;
        foreach(ApplianceFunctionSO functionSO in applianceFunctionSOs)
        {
            ApplianceFunction newFunction = new ApplianceFunction(functionSO);
            _applianceFunctions.Add(newFunction);
        }
    }

    public string GetApplianceName()
    {
        return _applianceSO.applianceName;
    }

    public List<ApplianceFunction> GetApplianceFunctions()
    {
        return _applianceFunctions;
    }
}
