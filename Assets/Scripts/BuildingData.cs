using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "ScriptableObjects/BuildingData", order = 51)]
public class BuildingData : ScriptableObject
{
    public BuildingType buildingType;
    public List<ResourceCost> costs;
}