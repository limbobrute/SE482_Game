using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDataNew", menuName = "ScriptableObjects/BuildingDataNew", order = 1)]
public class BuildingDataNew : ScriptableObject
{
    [Serializable] public struct Cost
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

    [Header("General Info")]
    public string buildingName;
    public int buildingLevel;
    public Cost cost;
    public GameObject buildingPrefab;
    public Sprite icon;
    public string description = "Short Description";

    [Header("Next Level")]
    public BuildingDataNew nextLevel;
    public bool canLevelUp;

    [Header("Buff Properties")]
    public int flatResourceIncrement; // Buff 1
    //public ResourceType secondaryResourceType; // Buff 2
    //public float surroundingBuildingBuffPercentage; // Buff 3

    [Header("Manpower Slot")]
    public ManpowerSlot[] slots; // Reference to the building slot
}
