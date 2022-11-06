using System;
using UnityEngine;
using UnityEngine.UI;

public class ApplianceFunction
{
    public bool working = false;
    public float progress = 0;
    public GameObject progressBar;

    private ApplianceFunctionSO _applianceFunctionSO;
    private int _startTime;
    private int _endTime;

    private Movement playerMovement;

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

        _startTime = TimeManager.GetAbsoluteGameMinutes();
        progress = 0f;

        //Grab player movement and lock movement if manual task
        if(_applianceFunctionSO.manual)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            playerMovement.movementLocked = true;
        }

        // We should remove the input items from the inventory and begin the function
        // After the amount of hours passes, the function should complete, the appliance becomes available
        // and the output items should be added to inventory
    }

    public void UpdateFunction()
    {
        int minutesToCompleteFunction = (int)Math.Floor(_applianceFunctionSO.hoursToMake * 60);
        progress = (float)(TimeManager.GetAbsoluteGameMinutes() - _startTime) / minutesToCompleteFunction;

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

        //If manual unlock player movement
        if (_applianceFunctionSO.manual)
        {
            playerMovement.movementLocked = false;
        }
    }
}
