using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Clock : MonoBehaviour
{
    public int minutesToRefresh = 15;
    public TextMeshProUGUI clockText;

    private void OnEnable()
    {
        EventHandler.AdvanceGameMinuteEvent += ConfigureClockUIForTime;
    }

    private void OnDisable()
    {
        EventHandler.AdvanceGameMinuteEvent -= ConfigureClockUIForTime;
    }

    private void ConfigureClockUIForTime(int gameMinute, int gameHour)
    {
        if (gameMinute % minutesToRefresh != 0) return;

        bool isPM = gameHour >= 12;
        string readableHoursString = isPM && gameHour != 12 ? (gameHour - 12).ToString() : gameHour.ToString();
        string readableMinutesString = gameMinute.ToString("D2");
        string readableAMPMString = isPM ? "PM" : "AM";
        clockText.text = readableHoursString + ":" + readableMinutesString + " " + readableAMPMString;
    }

}
