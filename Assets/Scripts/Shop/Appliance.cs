using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance
{
    private ApplianceSO _applianceSO;
    private List<ApplianceFunction> _applianceFunctions = new List<ApplianceFunction>();
    
    public WorldAppliance worldAppliance;
    private ApplianceFunction currentFunction;

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

    public void FunctionClicked(ApplianceFunction function)
    {
        currentFunction = function;
        worldAppliance.MovePlayerToAppliance();
    }

    public void StartFunction()
    {
        //Check other functions to see if they are running
        bool anyFunctionRunning = false;
        foreach (ApplianceFunction f in _applianceFunctions)
        {
            if (f.working)
                anyFunctionRunning = true;
        }

        //If there are no functions running on this appliance start
        if(!anyFunctionRunning)
            currentFunction.StartFunction();
    }
}
