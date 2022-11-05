using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager: MonoBehaviour
{
    public float secondsPerGameHour = 8f;

    private int _gameHour = 6;
    private bool _gameClockPaused = false;
    private float _gameTick = 0f;

    // Singleton pattern
    // TODO?: Make abstract Singleton class?
    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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

        if (_gameTick >= secondsPerGameHour)
        {
            _gameTick -= secondsPerGameHour;
            _gameHour++;
            if (_gameHour >= 24)
            {
                _gameHour = 0;
            }

            UpdateGameHour();
        }
    }

    private void UpdateGameHour()
    {
        EventHandler.CallAdvanceGameHourEvent(_gameHour);
    }

    private void Start()
    {
        EventHandler.CallAdvanceGameHourEvent(_gameHour);
    }

    public static int GetGameHour()
    {
        return _instance._gameHour;
    }

    public static float GetGameSeconds()
    {
        return _instance._gameHour * _instance.secondsPerGameHour + _instance._gameTick;
    }

    public static float GetSecondsPerHour()
    {
        return _instance.secondsPerGameHour;
    }

    public static int HoursBetween(int startTime, int endTime)
    {
        if (startTime <= endTime)
        { 
            return endTime - startTime;
        }
        else
        {
            return endTime + (24 - startTime);
        }
    }

    public static float SecondsBetween(float startTime, float endTime)
    {
        if (startTime <= endTime)
        {
            return endTime - startTime;
        }
        else
        {
            return endTime + ((24 * _instance.secondsPerGameHour) - startTime);
        }
    }

    public static int AddHours(int time1, int time2)
    {
        time1 += time2;
        if (time1 >= 24)
        {
            time1 -= 24;
        }
        return time1;
    }

    public static float AddSeconds(float time1, float time2)
    {
        time1 += time2;
        if (time1 >= (24 * _instance.secondsPerGameHour))
        {
            time1 -= (24 * _instance.secondsPerGameHour);
        }
        return time1;
    }
}
