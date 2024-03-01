using UnityEngine;
using System.Collections.Generic;
using Enums;
using System.Linq;
using System;

public class Buildings : MonoBehaviour
{
    public string buildingName; 
    public Dictionary<ResourceType, int> Costs { get; set; }
    public int rate;
    [field: SerializeField]
    public BuildingData BuildingData { get; set; }

    private void Start()
    {
        Costs = new Dictionary<ResourceType, int>();
        DefineCosts();
    }
    // Method to produce resources over time
    public void ProduceResources()
    {
        // Produce resources over time
    }

    public void DefineCosts() {
         Costs = BuildingData.costs.ToDictionary(rc => rc.resourceType, rc => rc.cost);
        buildingName = BuildingData.buildingType.ToString();
    }

    public void PrintBuildingCosts()
    {
        foreach (var cost in Costs)
        {
            string key = cost.Key.ToString();
            int value = cost.Value;
            UnityEngine.Debug.Log($"Key: {key}, Value: {value}");
        }
    }

    public void DecreaseCosts(float decreasePercentage) {
        //foreach (var cost in Costs)
        //{
        //    if (decreasePercentage < 1 && decreasePercentage > 0) {
        //        float newValue = cost.Value * decreasePercentage;

        //        Costs[cost.Key] = (int)newValue;
        //            }
        //    else
        //    {
        //        throw new Exception("decrease percentage is not decreasing");
        //    }

        //}
        // Use a for loop to iterate over the dictionary keys
        for (int i = 0; i < Costs.Keys.Count; i++)
        {
            // Get the current key
            var key = Costs.Keys.ElementAt(i);

            // Get the current value
            var value = Costs[key];

            // Check the decrease percentage
            if (decreasePercentage < 1 && decreasePercentage > 0)
            {
                // Calculate the new value
                float newValue = value * decreasePercentage;

                // Update the value in the dictionary
                Costs[key] = (int)newValue;
            }
            else
            {
                // Throw an exception
                throw new Exception("decrease percentage is not decreasing");
            }
        }

    }
}

