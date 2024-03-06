using UnityEngine;
using System.Collections.Generic;
using Enums;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    private void Start()
    {
        AddResource(ResourceType.Metal, 10);
        AddResource(ResourceType.Housing, 10);
        AddResource(ResourceType.Workforce, 10);
        AddResource(ResourceType.Energy, 10);
        AddResource(ResourceType.BioOrganics, 10);
    }

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

    public void printResources() {
        foreach (KeyValuePair<ResourceType, int> item in resources)
        {
            Debug.Log( item.Key +" "+ item.Value);
        }
    }
}
