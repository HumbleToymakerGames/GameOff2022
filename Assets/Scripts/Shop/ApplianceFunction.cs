using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplianceFunction
{
    private ApplianceFunctionSO _applianceFunctionSO;
    public bool working = false;
    private float startTime;
    private float endTime;

    public float progress = 0;
    public GameObject progressBar;

    public ApplianceFunction(ApplianceFunctionSO so)
    {
        _applianceFunctionSO = so;
    }

    public string GetApplianceFunctionName()
    {
        return _applianceFunctionSO.functionName;
    }

    public void StartFunction(GameObject progressBar)
    {
        Debug.Log("Performing function " + GetApplianceFunctionName());
        this.progressBar = progressBar;
        working = true;

        startTime = TimeManager.GetGameSeconds();
        progress = 0;

        // We should remove the input items from the inventory and begin the function
        // After the amount of hours passes, the function should complete, the appliance becomes available
        // and the output items should be added to inventory
    }

    public void UpdateFunction()
    {
        endTime = TimeManager.AddSeconds(startTime, _applianceFunctionSO.hoursToMake * TimeManager.GetSecondsPerHour());
        progress = TimeManager.SecondsBetween(startTime, TimeManager.GetGameSeconds()) / TimeManager.SecondsBetween(startTime, endTime);

        progressBar.GetComponent<Slider>().value = progress;
        if (progress >= 1)
        {
            FinishFunction();
        }
    }

    public void FinishFunction()
    {
        GameObject.Destroy(progressBar.gameObject);
        Debug.Log("Finished function " + GetApplianceFunctionName());
        working = false;
    }
}
