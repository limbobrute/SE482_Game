using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTimer : MonoBehaviour
{
    [Serializable]
    struct Timer
    {
        public int min;
        public int hour;
        public int day;
        public string ProjName;
    }
    public RTSTimer timer;
    int min;
    int hour;
    int day; 
    [SerializeField]List<Timer> projects = new List<Timer>();
    public void GetMin()
    { min = timer.GiveMin(); }

    public void GetHour(int adder)
    { hour = timer.GiveHour(); hour += adder; }

    public void GetDay(int adder)
    { day = timer.GiveDay(); day += adder; }

    public void AddProject(string name)
    {
        Timer proj = new Timer();
        if(hour > 24)
        { hour -= 24; day++; }
        proj.min = min;
        proj.hour = hour;
        proj.day = day;
        proj.ProjName = name;
        projects.Add(proj);
        StartCoroutine(ProjectChecker());
    }

    IEnumerator ProjectChecker()
    {
        while(projects.Count > 0)
        {
            foreach(Timer proj in projects)
            {
                var m = timer.GiveMin();
                var h = timer.GiveHour();
                var d = timer.GiveDay();
                if(proj.min == m && proj.hour == h && proj.day == d)
                { 
                    Debug.Log("Project " + proj.ProjName + " which started at "+proj.day.ToString()+":"+proj.hour.ToString()+":"+proj.min.ToString() + " is done!!");
                    projects.Remove(proj);
                }
            }
            yield return null;
        }
        StopCoroutine(ProjectChecker());
    }
}
