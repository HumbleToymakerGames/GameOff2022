using System;
using System.Collections;
using System.Collections.Generic;

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
}
