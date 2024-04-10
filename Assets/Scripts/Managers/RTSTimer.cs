using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class RTSTimer : MonoBehaviour
{
    public UnityEvent onFiveSeconds;
    int minute = 0;
    int hour = 0;
    int day = 0;
    float timer = 0f;
    public float delay = 2f;
    public TextMeshProUGUI text;
    public static RTSTimer Instance { get; private set; } // Singleton instance

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        text.text = "0:00:00";
        StartCoroutine(RTSClock());
    }

    public void Resume()
    { timer = 0f; StartCoroutine(RTSClock()); }

    IEnumerator RTSClock()
    {
        //Debug.Log("Delay timer: " + timer);
        while (timer < delay)
        { timer += Time.deltaTime; yield return null; }

        minute += 1;
        if (minute == 60)
        { minute = 0; hour += 1; }

        var Mstring = minute.ToString();
        var Hstring = hour.ToString();
        if (minute == 0)
        { Mstring = "00"; }
        else if (minute < 10)
        { Mstring = "0" + minute.ToString(); }

        if (minute % 5 == 0)
        {
            Debug.Log("Multiple of 5");
            onFiveSeconds?.Invoke();
        }

        if (hour < 10)
        { Hstring = "0" + hour.ToString(); }

        text.text = day.ToString() + ":" + Hstring + ":" + Mstring;

        if (hour == 24)
        {
            hour = 0;
            day += 1;
            Hstring = "00";
            Mstring = "00";
            text.text = day.ToString() + ":" + Hstring + ":" + Mstring;
            StopAllCoroutines();
        }
        else
        { timer = 0f; StartCoroutine(RTSClock()); }

    }

    public IEnumerator WaitForInGameSeconds(float seconds)
    {
        float startDay = day;
        float startHour = hour;
        float startMinute = minute;

        float endDay = startDay + Mathf.Floor(seconds / (60 * 24));
        float endHour = startHour + Mathf.Floor((seconds % (60 * 24)) / 60);
        float endMinute = startMinute + seconds % 60;

        if (endMinute >= 60)
        {
            endMinute -= 60;
            endHour += 1;
        }

        if (endHour >= 24)
        {
            endHour -= 24;
            endDay += 1;
        }

        while (day < endDay || (day == endDay && hour < endHour) || (day == endDay && hour == endHour && minute < endMinute))
        {
            yield return null;
        }
    }

    public int GiveMin()
    { return minute; }

    public int GiveHour()
    { return hour; }
    public int GiveDay()
    { return day; }
}
