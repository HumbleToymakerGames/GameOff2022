using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatusBar : MonoBehaviour
{
    public TextMeshProUGUI timeText = null;

    private void OnEnable()
    {
        EventHandler.AdvanceGameMinuteEvent += UpdateGameTimeUI;
    }

    private void OnDisable()
    {
        EventHandler.AdvanceGameMinuteEvent -= UpdateGameTimeUI;
    }

    private void UpdateGameTimeUI(int gameMinute, int gameHour)
    {
        // Parameters passed in through EventHandler.AdvanceGameMinuteEvent event
        string timeString = gameHour.ToString() + ":" + gameMinute.ToString("D2");
        timeText.text = timeString;
    }

}
