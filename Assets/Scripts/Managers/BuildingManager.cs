using UnityEngine;
using System.Collections.Generic;
using Enums;
using System.Linq;
using System.Diagnostics.Contracts;

public class BuildingManager : MonoBehaviour
{
    [field: SerializeField]
    public ResourceManager ResourceManager { get; set; }
    public Dictionary<BuildingType, Buildings> BuildingTypesToBuild { get; set; }
    public List<Buildings> BuildingsInScene { get; set; }

    public Vector3 instancePostition = Vector3.zero; 

    // Method to instantiate a building

    private void Start()
    {
        BuildingTypesToBuild = new Dictionary<BuildingType, Buildings>
        {
            //buildings.Costs.Add(ResourceType.Workforce, 3);
            //BuildingTypes.Add(BuildingType.Agriculture, buildings);
            { BuildingType.Agriculture, Resources.Load<Buildings>("Agriculture") }
        };
        BuildingsInScene = new();
    }
    public void InstantiateBuilding(int buildingType)
    {
        BuildingType bt = (BuildingType)buildingType;
        // Check if the dictionary contains the requested building type
        if (BuildingTypesToBuild.ContainsKey(bt))
        {
            // Get the prefab for the building type
            Buildings buildingPrefab = BuildingTypesToBuild[bt];

            if(buildingPrefab.Costs ==  null)
            {
            buildingPrefab.DefineCosts();
            }
            // Instantiate the prefab in the game scene
            // You can adjust the position, rotation and parent parameters as needed
             Instantiate(buildingPrefab, instancePostition, Quaternion.identity, null);

            // Deduct the resources from the ResourceManager according to the costs of the building
            foreach (var cost in buildingPrefab.Costs)
            {
                ResourceType resourceType = cost.Key;
                int costValue = cost.Value;
                ResourceManager.DeductResource(resourceType, costValue);
            }

            // Add the instantiated building object to the list of buildings in the scene
            BuildingsInScene.Add(buildingPrefab);
        }
        else
        {
            // Handle the case when the building type is not found in the dictionary
            // For example, log an error message
            Debug.LogError($"Building type {buildingType} not found in the dictionary.");
        }
    }

    public void IncreaseProductionRate(BuildingType buildingType, int rate)
    {
        foreach (var building in BuildingsInScene)
        {
            if (building.buildingName == buildingType.ToString())
            {
                building.rate = building.rate+(building.rate * rate);
            }
        }
    }

    public void DecreaseBuildingCosts(int buildingType) 
    {
        BuildingType bt = (BuildingType)buildingType;
        //foreach(var building in BuildingTypesToBuild)
        //{
        //    if(bt == building.Key)
        //    {
        //        var build = BuildingTypesToBuild[building.Key];
        //        build.DefineCosts();
        //        build.DecreaseCosts(.8f);

        //        BuildingTypesToBuild[building.Key] = build;
        //        Debug.Log("Building Cost Decreased");
        //    }
        //}
        // Use a for loop to iterate over the dictionary keys
        for (int i = 0; i < BuildingTypesToBuild.Keys.Count; i++)
        {
            // Get the current key
            var key = BuildingTypesToBuild.Keys.ElementAt(i);

            // Check if the key matches the building type
            if (bt == key)
            {
                // Get the current value
                var build = BuildingTypesToBuild[key];

                // Modify the value
                //build.DefineCosts();
                build.DecreaseCosts(.8f);

                // Update the value in the dictionary
                BuildingTypesToBuild[key] = build;

                Debug.Log("Building Cost Decreased");
            }
        }
    }
}
