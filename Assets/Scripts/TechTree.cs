using UnityEngine;
using System.Collections.Generic;
using Enums;
using System.Linq;
using UnityEngine.Events;

[System.Serializable]
public struct ResourceCost
{
    public ResourceType resourceType;
    public int cost;
}

[System.Serializable]
public class ResourceCostEvent : UnityEvent<ResourceCost> { }
public class TechTree : MonoBehaviour
{
    [field: SerializeField]
    public ResourceManager ResourceManager { get; set; }
    [field: SerializeField]

    public BuildingManager buildingManager;


    // Public fields for the parameters
    public BuildingType buildingType;
    public List<ResourceCost> newCostsList;
    public int costToSet;
    public ResourceType resourceType;

    //needs to pass percentage and building specifications
    public void AdjustBuildingCosts()
    {
        // Convert the list to a dictionary
        //Dictionary<ResourceType, int> newCosts = newCostsList.ToDictionary(rc => rc.resourceType, rc => rc.cost);
        //newCosts[resourceType] = costToSet;

        //// Get the building from the BuildingManager
        //Buildings building = buildingManager.BuildingTypes[buildingType];

        //// Adjust the costs
        //foreach (var cost in newCosts)
        //{
        //    if (building.Costs.ContainsKey(cost.Key))
        //    {
        //        building.Costs[cost.Key] = cost.Value;
        //    }
        //    else
        //    {
        //        building.Costs.Add(cost.Key, cost.Value);
        //    }
        //}


        //call building manager to set decrease costs by percentage. 

    }
    public void SetValues(BuildingData buildingData)
    {
        buildingType = buildingData.buildingType;
        newCostsList = new List<ResourceCost>(buildingData.costs);
    }
    // Method to modify production rate of buildings
    public void ModifyProductionRate(BuildingType buildingType)
    {
        // Modify the production rate of the building
    }
    // Other methods...
}
