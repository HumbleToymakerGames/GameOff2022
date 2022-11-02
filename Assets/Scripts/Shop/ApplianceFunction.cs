using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceFunction
{
    private ApplianceFunctionSO _applianceFunctionSO;

    public ApplianceFunction(ApplianceFunctionSO so)
    {
        _applianceFunctionSO = so;
    }

    public string GetApplianceFunctionName()
    {
        return _applianceFunctionSO.functionName;
    }
}
