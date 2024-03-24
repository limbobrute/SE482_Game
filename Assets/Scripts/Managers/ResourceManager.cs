using UnityEngine;
using System.Collections.Generic;
using Enums;
using System;
using SerializableDictionary.Scripts;
using PlasticPipe.PlasticProtocol.Messages;

public class ResourceManager : MonoBehaviour
{
    [Serializable]
    public struct Cost
    {
        public int Housing;
        public int Metal;
        public int Energy;
        public int BioOrganics;
        public int Workforce;
    }
    [SerializeField]
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    [SerializeField] SerializableDictionary<string, Cost> BuidingCostTable = new SerializableDictionary<string, Cost>();

    private void Start()
    {
        AddResource(ResourceType.Metal, 10);
        AddResource(ResourceType.Housing, 10);
        AddResource(ResourceType.Workforce, 10);
        AddResource(ResourceType.Energy, 10);
        AddResource(ResourceType.BioOrganics, 10);
    }

    //Check to see if the given node can be researched
    public bool CanResearch(TechTreeNode node)
    {
        var cost = node.cost;
        int StoredHousing = resources[ResourceType.Housing];
        int StoredMetal = resources[ResourceType.Metal];
        int StoredEnergy = resources[ResourceType.Energy];
        int StoredBioOrganics = resources[ResourceType.BioOrganics];
        int StoredWorkforce = resources[ResourceType.Workforce];

        if (StoredHousing >= cost.Housing 
            && StoredMetal >= cost.Metal 
            && StoredEnergy >= cost.Energy
            && StoredBioOrganics >= cost.BioOrganics
            && StoredWorkforce >= cost.Workforce)
        { return true; }
        else
        { return false; }
    }

    //Check to see if the given building can be built
    public bool CanBuild(string building)
    {
        int StoredHousing = resources[ResourceType.Housing];
        int StoredMetal = resources[ResourceType.Metal];
        int StoredEnergy = resources[ResourceType.Energy];
        int StoredBioOrganics = resources[ResourceType.BioOrganics];
        int StoredWorkforce = resources[ResourceType.Workforce];
        /*
         * The reason for the different method of getting the value by key
         * is due to the nature of the serializable Dictionary 
         */
        int AdjustedHousing = BuidingCostTable.Get(building).Housing;
        int AdjustedMetal = BuidingCostTable.Get(building).Metal;
        int AdjustedEnergy = BuidingCostTable.Get(building).Energy;
        int AdjustedBioOrganics = BuidingCostTable.Get(building).BioOrganics;
        int AdjustedWorkforce = BuidingCostTable.Get(building).Workforce;
        /*
         * Change the local Adjusted ints here based on the co-efficents in the buildingManager
         * by building and any golbal co-efficient
         */
        if (StoredHousing >= AdjustedHousing
            && StoredMetal >= AdjustedMetal
            && StoredEnergy >= AdjustedEnergy
            && StoredBioOrganics >= AdjustedBioOrganics
            && StoredWorkforce >= AdjustedWorkforce)
        { return true; }
        else
        { return false; }
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
        foreach (System.Collections.Generic.KeyValuePair<ResourceType, int> item in resources)
        {
            Debug.Log( item.Key +" "+ item.Value);
        }
    }
}
