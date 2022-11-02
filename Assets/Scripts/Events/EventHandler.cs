using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<int> AdvanceGameHourEvent;

    public static void CallAdvanceGameHourEvent(int gameHour)
    {
        if (AdvanceGameHourEvent != null)
        {
            AdvanceGameHourEvent(gameHour);
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
}
