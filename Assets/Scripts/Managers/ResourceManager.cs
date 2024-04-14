using UnityEngine;
using Enums;
using System;
using SerializableDictionary.Scripts;
using UnityEngine.Events;
using UnityEngine.UI;


public class ResourceManager : MonoBehaviour
{
    public BuildingManager buildingManager;
    public int wood;
    public int metal;
    public int crystal;
    public int synthia;
    public int workforce;

    [SerializeField]
    private int woodToAdd =0;
    [SerializeField]
    private int metalToAdd = 0;
    [SerializeField]
    private int synthiaToAdd = 0;
    [SerializeField]
    private int crystalToAdd = 0;
    [SerializeField]
    private int workforceToAdd = 0;//This one may be uneccessary


    [SerializeField] SerializableDictionary<string, Cost> BuidingCostTable = new SerializableDictionary<string, Cost>();

    public UnityEvent OnResourceUpdate;

    public int WoodToAdd { get => woodToAdd; set => woodToAdd = value; }
    public int MetalToAdd { get => metalToAdd; set => metalToAdd = value; }
    public int SynthiaToAdd { get => synthiaToAdd; set => synthiaToAdd = value; }
    public int CrystalToAdd { get => crystalToAdd; set => crystalToAdd = value; }
    public int WorkforceToAdd { get => workforceToAdd; set => workforceToAdd = value; }

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
    public bool CanBuild(Cost buildingCost)
    {
        int StoredWood = wood;
        int StoredMetal = metal;
        int StoredCrystal = crystal;
        int StoredSynthia = synthia;
        int StoredWorkforce = workforce;

        int AdjustedSynthia = buildingCost.Synthia;
        int AdjustedMetal = buildingCost.Metal;
        int AdjustedCrystal = buildingCost.Crystal;
        int AdjustedWood = buildingCost.Wood;
        int AdjustedWorkforce = buildingCost.Workforce;

        double adjuster = buildingManager.UniBuldingCoefficient/100;
        AdjustedWood = (int)(AdjustedWood * adjuster);
        AdjustedMetal = (int)(AdjustedMetal * adjuster);
        AdjustedCrystal = (int)(AdjustedCrystal * adjuster);
        AdjustedSynthia = (int)(AdjustedSynthia * adjuster);
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

    public void ChangeResourceGain(TechTreeNode node)
    {
        var Dic = node.Bonuses;
        string resource = null;
        if (Dic.ContainsKey("Wood"))
        { resource = "Wood"; }
        else if (Dic.ContainsKey("Metal"))
        { resource = "Metal"; }
        else if (Dic.ContainsKey("Crystal"))
        { resource = "Crystal"; }
        else if (Dic.ContainsKey("Synthia"))
        { resource = "Synthia"; }
        else if(Dic.ContainsKey("Uni"))
        { resource = "Uni"; }

        int mod = Dic.Get(resource);
        double adjuster = mod / 100;
        if(resource == "Uni")
        {
            WoodToAdd += (int)(WoodToAdd * adjuster);
            MetalToAdd += (int)(MetalToAdd * adjuster);
            CrystalToAdd += (int)(CrystalToAdd * adjuster);
            SynthiaToAdd += (int)(SynthiaToAdd * adjuster);
        }
        //else if()//Add logic for resource specific modeifiers
    }

    // Method to add resources
    public void AddResource()
    {
        wood += WoodToAdd;
        metal += MetalToAdd;
        crystal += CrystalToAdd;
        synthia += SynthiaToAdd;

        OnResourceUpdate?.Invoke();
    }

   public void ReallocateBuilder(int builders)
    {
        workforce += builders;
    }

    // Method to deduct resources
    public void DeductResource(Cost resourceCost)
    {
        synthia -= resourceCost.Synthia;
        crystal -= resourceCost.Crystal;
        metal -= resourceCost.Metal;
        wood -= resourceCost.Wood;
        workforce -= resourceCost.Workforce;
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
