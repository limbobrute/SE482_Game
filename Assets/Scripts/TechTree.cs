using UnityEngine;
using System.Collections.Generic;
using Enums;

public class TechTree : MonoBehaviour
{
    public ResourceManager ResourceManager { get; set; }
    public BuildingManager BuildingManager { get; set; }

    // Method to adjust building costs
    public void AdjustBuildingCosts(BuildingType buildingType, Dictionary<ResourceType, int> newCosts)
    {
        Buildings buildings = BuildingManager.BuildingTypes[buildingType];

        foreach (var cost in newCosts)
        {
            if (buildings.Costs.ContainsKey(cost.Key))
            {
                buildings.Costs[cost.Key] = cost.Value;
            }
            else
            {
                buildings.Costs.Add(cost.Key, cost.Value);
            }
        }
    }
    // Method to modify production rate of buildings
    public void ModifyProductionRate(BuildingType buildingType)
    {
        // Modify the production rate of the building
    }
    // Other methods...
}
