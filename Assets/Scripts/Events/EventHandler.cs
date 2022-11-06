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
}
