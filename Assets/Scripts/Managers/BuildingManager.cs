using UnityEngine;
using System.Collections.Generic;
using Enums;

public class BuildingManager : MonoBehaviour
{
    public ResourceManager ResourceManager { get; set; }
    public Dictionary<BuildingType, Buildings> BuildingTypes { get; set; }

    // Method to instantiate a building
    public void InstantiateBuilding(BuildingType buildingType)
    {
        // Instantiate the building and deduct the resources
    }
}
