using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/BuildingDataNew", order = 1)]
public class BuildingDataNew : ScriptableObject
{
    [Header("General Info")]
    public string buildingName;
    public int buildingLevel;
    public GameObject buildingPrefab;
    public Sprite icon;

    [Header("Next Level")]
    public BuildingDataNew nextLevel;
    public bool canLevelUp;

    [Header("Material Cost")]
    public int woodCost;
    public int crystalCost;
    public int metalCost;
    public int synthiaCost;

    [Header("Builder Cost")]
    public int builderCost;
    public int currentManpower;

    [Header("Time Cost")]
    public float constructionTime;

    [Header("Buff Properties")]
    public int flatResourceIncrement; // Buff 1
    //public ResourceType secondaryResourceType; // Buff 2
    //public float surroundingBuildingBuffPercentage; // Buff 3

    [Header("Manpower Slot")]
    public ManpowerSlot[] slots; // Reference to the building slot
}
