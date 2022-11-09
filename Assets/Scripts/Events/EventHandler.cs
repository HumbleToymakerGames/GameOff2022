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

    public static event Action<Appliance, Vector2> DidClickApplianceEvent;

    public static void CallDidClickApplianceEvent(Appliance clickedAppliance, Vector2 screenPosition)
    {
        if (DidClickApplianceEvent != null)
        {
            DidClickApplianceEvent(clickedAppliance, screenPosition);
        }
    }

    public static event Action<ApplianceFunction, ItemQuantity> ApplianceFunctionDidCompleteEvent;

    public static void CallApplianceFunctionDidCompleteEvent(ApplianceFunction completedApplianceFunction, ItemQuantity itemQuantity)
    {
        if (ApplianceFunctionDidCompleteEvent != null)
        {
            ApplianceFunctionDidCompleteEvent(completedApplianceFunction, itemQuantity);
        }
    }
}
