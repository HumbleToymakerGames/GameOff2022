using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Clock : MonoBehaviour
{
    public int minutesToRefresh = 15;
    private TextMeshProUGUI _clockText;

    private void Start()
    {
        _clockText = GetComponentInChildren<TextMeshProUGUI>();
    }

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
        if (gameMinute == 0 || gameMinute % minutesToRefresh != 0) return;

        bool isPM = gameHour > 12;
        string readableHoursString = isPM ? (gameHour - 12).ToString() : gameHour.ToString();
        string readableMinutesString = gameMinute.ToString("D2");
        string readableAMPMString = isPM ? "PM" : "AM";
        _clockText.text = readableHoursString + ":" + readableMinutesString + " " + readableAMPMString;
    }

}
