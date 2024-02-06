using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent : MonoBehaviour
{
    private int day = 0;
    private int hour = 0;
    private int minute = 0;

    public int GetDay()
    { return day; }

    public int GetHour()
    { return hour; }

    public int GetMin()
    { return minute; }

    public void SetTime(int d, int h, int m)
    { day = d; hour = h; minute = m; }
}
