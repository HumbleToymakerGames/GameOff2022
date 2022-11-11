using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance
{
    private ApplianceSO _applianceSO;
    private List<ApplianceFunction> _applianceFunctions = new List<ApplianceFunction>();
    
    public WorldAppliance worldAppliance;
    private ApplianceFunction currentFunction;

    public bool anyFunctionRunning;

    public Appliance(ApplianceSO applianceSO)
    {
        _applianceSO = applianceSO;
        List<ApplianceFunctionSO> applianceFunctionSOs = _applianceSO.functions;
        foreach(ApplianceFunctionSO functionSO in applianceFunctionSOs)
        {
            ApplianceFunction newFunction = new ApplianceFunction(functionSO, this);
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

    public void StartFunction()
    {
        //Check other functions to see if they are running
        anyFunctionRunning = false;
        foreach (ApplianceFunction f in _applianceFunctions)
        {
            if (f.working)
                anyFunctionRunning = true;
        }

        //If there are no functions running on this appliance start
        if (!anyFunctionRunning)
        {
            // currentFunction.StartFunction(UIManager.Instance.PlaceProgressBarForApplianceFunction(currentFunction, worldAppliance.transform.position + new Vector3(0, 0.25f, 0)));
            anyFunctionRunning = true;
            currentFunction = null;
        }
    }

    public void FunctionClicked(ApplianceFunction function)
    {
        currentFunction = function;
        UIManager.Instance.CloseApplianceContextPanel();
        worldAppliance.MovePlayerToAppliance();
    }


    public void UpdateFunction()
    {
        anyFunctionRunning = false;
        foreach (ApplianceFunction f in _applianceFunctions)
        {
            if (f.working)
            {
                anyFunctionRunning = true;
                f.UpdateFunction();
            }
        }
    }
}
