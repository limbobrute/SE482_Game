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
        if(minute == 0)
        { Mstring = "00"; }
        else if(minute < 10)
        { Mstring = "0" + minute.ToString(); }

            if (minute % 5 == 0)
            {
                Debug.Log("Multiple of 5");
            }

        if(hour < 10)
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

    public int GiveMin()
    { return minute; }

    public int GiveHour()
    { return hour; }
    public int GiveDay()
    { return day; }
}
