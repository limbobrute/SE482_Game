using UnityEngine;
using System.Collections.Generic;
using Enums;
using System;
using SerializableDictionary.Scripts;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine.Events;
using UnityEngine.UI;
using Codice.CM.Client.Differences;

[CreateAssetMenu(fileName = "NewNodeCost", menuName = "ScriptableObjects/NodeCost", order = 52)]
public class NodeCost : ScriptableObject
{
    public List<ResourceCost> costs;
}
public class ResourceManager : MonoBehaviour
{
    [Serializable]
    public struct Cost
    {
        public int Housing;
        public int Metal;
        public int Energy;
        public int Wood;
        public int Workforce;
    }
    [SerializeField]
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    [SerializeField] SerializableDictionary<string, Cost> BuidingCostTable = new SerializableDictionary<string, Cost>();

    public UnityEvent OnResourceUpdate;

    private void Start()
    {
        AddResource(ResourceType.Metal, 50);
        AddResource(ResourceType.Wood, 50);
        AddResource(ResourceType.Workforce, 10);
        AddResource(ResourceType.Crystal, 50);
        AddResource(ResourceType.Synthia, 25);
    }

    //Check to see if the given node can be researched
    public bool CanResearch(TechTreeNode node)
    {
        var cost = node.cost;
        int StoredHousing = resources[ResourceType.Wood];
        int StoredMetal = resources[ResourceType.Metal];
        int StoredEnergy = resources[ResourceType.Crystal];
        int StoredBioOrganics = resources[ResourceType.Synthia];
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
        int StoredHousing = resources[ResourceType.Wood];
        int StoredMetal = resources[ResourceType.Metal];
        int StoredEnergy = resources[ResourceType.Crystal];
        int StoredBioOrganics = resources[ResourceType.Synthia];
        int StoredWorkforce = resources[ResourceType.Workforce];
        /*
         * The reason for the different method of getting the value by key
         * is due to the nature of the serializable Dictionary 
         */
        int AdjustedHousing = BuidingCostTable.Get(building).Housing;
        int AdjustedMetal = BuidingCostTable.Get(building).Metal;
        int AdjustedEnergy = BuidingCostTable.Get(building).Energy;
        int AdjustedBioOrganics = BuidingCostTable.Get(building).Wood;
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

        OnResourceUpdate?.Invoke();
    }

    // Method to deduct resources
    public void DeductResource(ResourceType resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType) && resources[resourceType] >= amount)
        {
            resources[resourceType] -= amount;
            OnResourceUpdate?.Invoke();
        }
        else
        {
            throw new Exception("Not enough resources");
        }
    }
    public void DeductResource(NodeCost cost)
    {
        foreach (var price in cost.costs)
        {
            DeductResource(price.resourceType, price.cost);
        }
    }

    public void printResources() {
        var woodText = GameObject.Find("Resource Panel/Wood/Wood Text")?.GetComponent<Text>();
        var metalText = GameObject.Find("Resource Panel/Metal/Metal Text")?.GetComponent<Text>();
        var synthiaText = GameObject.Find("Resource Panel/Synthia/Synthia Text")?.GetComponent<Text>();
        var crystalText = GameObject.Find("Resource Panel/Crystal/Crystal Text")?.GetComponent<Text>();
        var workforceText = GameObject.Find("Resource Panel/Workforce/Workforce Text")?.GetComponent<Text>();

        if (woodText != null)
            woodText.text = resources[ResourceType.Wood].ToString();
        if (metalText != null)
            metalText.text = resources[ResourceType.Metal].ToString();
        if (synthiaText != null)
            synthiaText.text = resources[ResourceType.Synthia].ToString();
        if (crystalText != null)
            crystalText.text = resources[ResourceType.Crystal].ToString();
        if (workforceText != null)
            workforceText.text = resources[ResourceType.Workforce].ToString();
    }
}
