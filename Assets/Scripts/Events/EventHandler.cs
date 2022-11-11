using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action<int, int> AdvanceGameMinuteEvent;

    public static void CallAdvanceGameMinuteEvent(int gameMinute, int gameHour)
    {
        if (AdvanceGameMinuteEvent != null)
        {
            AdvanceGameMinuteEvent(gameMinute, gameHour);
        }
    }

    public static event Action<Appliance> DidClickApplianceEvent;

    public static void CallDidClickApplianceEvent(Appliance clickedAppliance)
    {
        if (DidClickApplianceEvent != null)
        {
            DidClickApplianceEvent(clickedAppliance);
        }
    }

    public static event Action<ApplianceFunction, SlotClass> ApplianceFunctionDidCompleteEvent;

    public static void CallApplianceFunctionDidCompleteEvent(ApplianceFunction completedApplianceFunction, SlotClass itemQuantity)
    {
        if (ApplianceFunctionDidCompleteEvent != null)
        {
            ApplianceFunctionDidCompleteEvent(completedApplianceFunction, itemQuantity);
        }
    }

    public static event Action<int, int> ShopMoneyDidChangeEvent;

    public static void CallShopMoneyDidChangeEvent(int netChange, int newTotal)
    {
        if (ShopMoneyDidChangeEvent != null) ShopMoneyDidChangeEvent(netChange, newTotal);
    }
}
