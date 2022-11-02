using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatusBar : MonoBehaviour
{
    public TextMeshProUGUI timeText = null;

    private void OnEnable()
    {
        EventHandler.AdvanceGameHourEvent += UpdateGameTimeUI;
    }

    private void OnDisable()
    {
        EventHandler.AdvanceGameHourEvent -= UpdateGameTimeUI;
    }

    private void UpdateGameTimeUI(int gameHour)
    {
        // Parameters passed in through EventHandler.AdvanceGameHourEvent event
        string timeString = gameHour.ToString() + ":00";
        timeText.text = timeString;
    }

}
