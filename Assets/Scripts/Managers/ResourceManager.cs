using UnityEngine;
using System.Collections.Generic;
using Enums;
using System;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    // Method to add resources
    public void AddResource(ResourceType resourceType, int amount)
    {
        if (!resources.ContainsKey(resourceType))
        {
            resources[resourceType] = 0;
        }

        resources[resourceType] += amount;
    }

    // Method to deduct resources
    public void DeductResource(ResourceType resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType) && resources[resourceType] >= amount)
        {
            resources[resourceType] -= amount;
        }
        else
        {
            throw new Exception("Not enough resources");
        }
    }
}
