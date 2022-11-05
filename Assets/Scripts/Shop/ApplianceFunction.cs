using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceFunction
{
    private ApplianceFunctionSO _applianceFunctionSO;
    public bool working = false;

    public ApplianceFunction(ApplianceFunctionSO so)
    {
        _applianceFunctionSO = so;
    }

    public string GetApplianceFunctionName()
    {
        return _applianceFunctionSO.functionName;
    }

    public void StartFunction()
    {
        Debug.Log("Performing function " + GetApplianceFunctionName());
        working = true;
        // We should remove the input items from the inventory and begin the function
        // After the amount of hours passes, the function should complete, the appliance becomes available
        // and the output items should be added to inventory
    }
}
