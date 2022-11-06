using UnityEngine;





public class TimeManager: SingletonMonoBehaviour<TimeManager>

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
        }
    }

    private void UpdateGameMinute() => EventHandler.CallAdvanceGameMinuteEvent(_gameMinute, _gameHour);

    public static int GetGameHour() => Instance._gameHour % 24;

    public static int GetAbsoluteGameHour() => Instance._gameHour;

    public static int MinutesBetween(int startTime, int endTime) => endTime - startTime;

    public static int AddMinutes(int time1, int time2) => time1 += time2;

    public static int GetAbsoluteGameMinutes() => (Instance._gameHour * 60) + Instance._gameMinute;



    /* 
     * There shouldn't be a need to use GameSeconds because GameMinutes is the 
     * smallest unit we care about.
     * At 8 seconds per game hour, a game minute takes 
     * about 0.133 real life seconds.
     * 
    public static float GetGameSeconds()
    {
        return Instance._gameHour * Instance.secondsPerGameHour + Instance._gameTick;
    }

    public static float GetSecondsPerHour()
    {
        return Instance.secondsPerGameHour;
    }

    public static float SecondsBetween(float startTime, float endTime)
    {
        if (startTime <= endTime)
        {
            return endTime - startTime;
        }
        else
        {
            return endTime + ((24 * Instance.secondsPerGameHour) - startTime);
        }
    }

    public static float AddSeconds(float time1, float time2)
    {
        time1 += time2;
        if (time1 >= (24 * Instance.secondsPerGameHour))
        {
            time1 -= (24 * Instance.secondsPerGameHour);
        }
        return time1;
    }

    */

}
