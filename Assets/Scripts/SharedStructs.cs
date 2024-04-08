
using System;
using UnityEngine;

[Serializable]
public struct Cost
{
    [Header("Material Cost")]
    public int Wood;
    public int Crystal;
    public int Metal;
    public int Synthia;

    [Header("Builder Cost")]
    public int Workforce;

    [Header("Time Cost")]
    public float Time;
}
