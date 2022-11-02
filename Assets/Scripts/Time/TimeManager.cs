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
}
