using UnityEngine;
using Enums;
using System;
using SerializableDictionary.Scripts;
using UnityEngine.Events;
using UnityEngine.UI;


public class ResourceManager : MonoBehaviour
{
    [Serializable]
    public struct Cost
    {
        public int Synthia;
        public int Metal;
        public int Crystal;
        public int Wood;
        public int Workforce;
    }

    public int wood;
    public int metal;
    public int crystal;
    public int synthia;
    public int workforce;

    public int woodToAdd; 
    public int metalToAdd;
    public int synthiaToAdd;
    public int crystalToAdd;
    public int workforceToAdd;//This one may be uneccessary


    [SerializeField] SerializableDictionary<string, Cost> BuidingCostTable = new SerializableDictionary<string, Cost>();

    public UnityEvent OnResourceUpdate;


    private void Start()
    {
        metal = 50;
        wood = 50; 
        workforce = 10;
        crystal = 50;
        synthia = 25;
    }

    //Check to see if the given node can be researched
    public bool CanResearch(TechTreeNode node)
    {
        var cost = node.cost;
        int StoredSynthia = synthia;
        int StoredMetal = metal;
        int StoredCrystal = crystal;
        int StoredWood = wood;
        int StoredWorkforce = workforce;


        if (StoredSynthia >= cost.Synthia 
            && StoredMetal >= cost.Metal 
            && StoredCrystal >= cost.Crystal
            && StoredWood >= cost.Wood
            && StoredWorkforce >= cost.Workforce)
        { return true; }
        else
        { return false; }
    }

    //Check to see if the given building can be built
    public bool CanBuild(string building)
    {
        int StoredWood = wood;
        int StoredMetal = metal;
        int StoredCrystal = crystal;
        int StoredSynthia = synthia;
        int StoredWorkforce = workforce;
        /*
         * The reason for the different method of getting the value by key
         * is due to the nature of the serializable Dictionary 
         */
        int AdjustedSynthia = BuidingCostTable.Get(building).Synthia;
        int AdjustedMetal = BuidingCostTable.Get(building).Metal;
        int AdjustedCrystal = BuidingCostTable.Get(building).Crystal;
        int AdjustedWood = BuidingCostTable.Get(building).Wood;
        int AdjustedWorkforce = BuidingCostTable.Get(building).Workforce;
        /*
         * Change the local Adjusted ints here based on the co-efficents in the buildingManager
         * by building and any golbal co-efficient
         */
        if (StoredWood >= AdjustedWood
            && StoredMetal >= AdjustedMetal
            && StoredCrystal >= AdjustedCrystal
            && StoredSynthia >= AdjustedSynthia
            && StoredWorkforce >= AdjustedWorkforce)
        { return true; }
        else
        { return false; }
    }

    // Method to add resources
    public void AddResource()
    {
        wood += woodToAdd;
        metal += metalToAdd;
        crystal += crystalToAdd;
        synthia += synthiaToAdd;

        OnResourceUpdate?.Invoke();
    }

    // Method to deduct resources
    public void DeductResource()
    {
        throw new NotImplementedException("DeductResource is not implemented");
    }

    public void printResources() {
        var woodText = GameObject.Find("Resource Panel/Wood/Wood Text")?.GetComponent<Text>();
        var metalText = GameObject.Find("Resource Panel/Metal/Metal Text")?.GetComponent<Text>();
        var synthiaText = GameObject.Find("Resource Panel/Synthia/Synthia Text")?.GetComponent<Text>();
        var crystalText = GameObject.Find("Resource Panel/Crystal/Crystal Text")?.GetComponent<Text>();
        var workforceText = GameObject.Find("Resource Panel/Workforce/Workforce Text")?.GetComponent<Text>();

        if (woodText != null)
            woodText.text = wood.ToString();
        if (metalText != null)
            metalText.text = metal.ToString();
        if (synthiaText != null)
            synthiaText.text = synthia.ToString();
        if (crystalText != null)
            crystalText.text = crystal.ToString();
        if (workforceText != null)
            workforceText.text = workforce.ToString();
    }
}
