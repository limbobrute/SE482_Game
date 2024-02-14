using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : Events
{
    public bool CanAutomate = false;
    public struct ResourcesNeeded
    {
        public int Metal;
        public int Food;
        public int Water;
    }

    public struct TimeNeeded
    {
        public int Days;
        public int Hours;
        public int Minutes;
    }
}
