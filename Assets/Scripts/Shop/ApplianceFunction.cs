using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplianceFunction
{
    public bool working = false;
    public float progress = 0;
    public GameObject progressBar;
    public Appliance parentAppliance => _parentAppliance;

    private ApplianceFunctionSO _applianceFunctionSO;
    private Appliance _parentAppliance;
    private int _startTime;
    private int _endTime;

    private Movement playerMovement;


    public ApplianceFunction(ApplianceFunctionSO so, Appliance parentAppliance)
    {
        _parentAppliance = parentAppliance;
        _applianceFunctionSO = so;
    }

    public string GetApplianceFunctionName()
    {
        return _applianceFunctionSO.functionName;
    }

    public bool IsKnown()
    {
        return ShopManager.Instance.GetKnownRecipes().Contains(_applianceFunctionSO);
    }

    public void StartFunction(GameObject progressBar = null)
    {
        foreach(SlotClass slot in _applianceFunctionSO.inputItems)
        {
            InventoryManager.Instance.Remove(slot.GetItem(), slot.GetQuantity());
        }

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


        /*public bool CanCraft(InventoryManager inventory)

        { 

            for (int i = 0; i < inputItems.Length; i++)
            {
                if (!inventory.Contains(inputItems[i].GetItem(), inputItems[i].GetQuantity()))
                {

                     return false;
                }
            }

               //return if inventory has input itmes

            return true;
        }*/


        /*for (int i = 0; i < inputItems.Length; i++)
        {
            inventory.Remove(inputItems[i].GetItem(), inputItems[i].GetQuantity());
        }*/

    }
    public bool CanAffordFunction()
    {
        foreach (SlotClass item in _applianceFunctionSO.inputItems)
        {
           if (InventoryManager.Instance.CanAfford(item) == false) return false;
        }
        return true;
    }
         

    public void UpdateFunction()
    {
        
        int minutesToCompleteFunction = (int)Math.Floor(_applianceFunctionSO.hoursToMake * 60);
        progress = (float)(TimeManager.GetAbsoluteGameMinutes() - _startTime) / minutesToCompleteFunction;

        if(progressBar != null)
            progressBar.GetComponent<ApplianceFunctionProgressBar>().progress = progress;

        if (progress >= 1)
        {
            FinishFunction();
        }
    }

    public void FinishFunction()
    {
        if(progressBar != null)
            progressBar.SetActive(false);
        EventHandler.CallApplianceFunctionDidCompleteEvent(this, GetItemQuantityForOutput());
        working = false;
        if (_applianceFunctionSO.manual) playerMovement.movementLocked = false;
    }

    public Sprite SpriteForOutputProduct()
    {
        if (GetItemQuantityForOutput() == null) return null;
        return GetItemQuantityForOutput().GetItem().itemIcon;
    }

    public List<SlotClass> GetItemQuantitiesForInputs()
    {
        return _applianceFunctionSO.inputItems;
    }

    public SlotClass GetItemQuantityForOutput()
    {
        return _applianceFunctionSO.outputItem;
    }

    public String GetDurationString()
    {
        int hours = (int)Math.Floor(_applianceFunctionSO.hoursToMake);
        int mins = (int)_applianceFunctionSO.hoursToMake % 1;
        return hours.ToString() + "h " + mins.ToString() + "m"; 
    }
}
