using System.Collections;
using UnityEngine;


public class Timer : MonoBehaviour
{
    public static Timer instance;

    public GameTime time;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(TimerCoroutine());
    }


    public string GetTime()
    {
        return time.ToString("mm:ss");
    }

    private IEnumerator TimerCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1);

            time.Seconds++;
        }
    }
}

public struct GameTime
{
    private int _minutes;
    private int _seconds;

    public int Minutes
    {
        get => _minutes;
        set => _minutes = value;
    }

    public int Seconds
    {
        get => _seconds;
        set
        {
            switch (value)
            {
                case >= 60:
                    _seconds = value - 60;
                    _minutes++;
                    break;
                case < 0:
                    _seconds = value + 60;
                    _minutes--;
                    break;
                default:
                    _seconds = value;
                    break;
            }
        }
    }

    public static GameTime operator -(GameTime a, GameTime b)
    {
        GameTime c = new()
        {
            Seconds = a.Seconds - b.Seconds,
            Minutes = a.Minutes - b.Minutes,
        };

        return c;
    }

    public static GameTime FormatTime(int time) => new GameTime(time);

    public GameTime(int time)
    {
        _seconds = time % 60;
        _minutes = time / 60;
    }
    
    public string ToString(string format)
    {
        format = format.Replace("mm", Minutes.ToString("00"));
        format = format.Replace("ss", Seconds.ToString("00"));

        return format;
    }

    public int ToSeconds() => Minutes * 60 + Seconds;
}  