using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : BaseEvent
{
    private bool isPaused = false;
    private int EndDay;
    private int EndHour;
    private int EndMinute;

    public void SetStartTime(int d, int h, int m)
    { SetTime(d, h, m); }

    public void SetEndTime(int ad, int ah, int am)
    {
        EndDay = GetDay() + ad;
        EndHour = GetHour() + ah;
        EndMinute = GetMin() + am;
    }

    public void ChangePause()
    { isPaused = !isPaused; }

    public void CancelEvent()
    { 
        //TODO: Method to cancel this event from happening
    }

    public void Completion()
    {
        //TODO: get precentage based on time start and time complete
    }
}
