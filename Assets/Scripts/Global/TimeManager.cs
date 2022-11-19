using System;
using UnityEngine;

public struct GameTime
{
    public int hour;
    public int minute;

    public static bool operator <(GameTime gt1, GameTime gt2)
    {
        return ((gt1.hour * 60) + gt1.minute) < ((gt2.hour * 60) + gt2.minute);
    }

    public static bool operator >(GameTime gt1, GameTime gt2)
    {
        return ((gt1.hour * 60) + gt1.minute) > ((gt2.hour * 60) + gt2.minute);
    }

    public static bool operator ==(GameTime gt1, GameTime gt2)
    {
        return gt1.hour == gt2.hour && gt1.minute == gt2.minute;
    }

    public static bool operator !=(GameTime gt1, GameTime gt2)
    {
        return gt1.hour != gt2.hour || gt1.minute != gt2.minute;
    }

}

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    public float secondsPerGameHour = 8f;

    private int _gameHour = 6;
    private int _gameMinute = 0;

    private bool _gameClockPaused = false;
    private float _gameTick = 0f;

    private void Start()
    {
        EventHandler.CallAdvanceGameMinuteEvent(_gameMinute, _gameHour);
    }

    private void Update()
    {
        if (!_gameClockPaused)
        {
            GameTick();
        }
    }

    private void GameTick()
    {
        _gameTick += Time.deltaTime;

        if (_gameTick >= (secondsPerGameHour / 60))
        {
            _gameTick -= (secondsPerGameHour / 60);
            _gameMinute++;

            if (_gameMinute >= 60)
            {
                _gameMinute = 0;
                _gameHour++;
            }

            UpdateGameMinute();
            if (GetGameHour() == 4 && _gameMinute == 0) EventHandler.CallIngredientSupplyDeliveryWasMadeEvent();
        }
    }

    private void UpdateGameMinute() => EventHandler.CallAdvanceGameMinuteEvent(_gameMinute, GetGameHour());

    public static int GetGameHour() => Instance._gameHour % 24;

    public static int GetAbsoluteGameHour() => Instance._gameHour;

    public static int MinutesBetween(int startTime, int endTime) => endTime - startTime;

    public static int AddMinutes(int time1, int time2) => time1 += time2;

    public static int GetAbsoluteGameMinutes() => (Instance._gameHour * 60) + Instance._gameMinute;

    public static GameTime GetCurrentGameTime()
    {
        GameTime resultGT;
        resultGT.hour = Instance._gameHour;
        resultGT.minute = Instance._gameMinute;
        return resultGT;
    }

    public static GameTime AddGameTimes(GameTime gt1, GameTime gt2)
    {
        GameTime resultGT = gt1;
        resultGT.hour += gt2.hour;
        resultGT.minute += gt2.minute;
        if (resultGT.minute >= 60)
        {
            resultGT.hour += resultGT.minute / 60;
            resultGT.minute = resultGT.minute % 60;
        }
        return resultGT;
    }

    public static GameTime AddMinutesToGameTime(GameTime gt, int minutes)
    {
        GameTime resultGT = gt;
        resultGT.minute += minutes;
        if (resultGT.minute >= 60)
        {
            resultGT.hour += resultGT.minute / 60;
            resultGT.minute = resultGT.minute % 60;
        }
        return resultGT;
    }

    public static GameTime ConvertMinutesToGameTime(int minutes)
    {
        GameTime resultGT;
        resultGT.hour = minutes / 60;
        resultGT.minute = minutes % 60;
        return resultGT;
    }

}
