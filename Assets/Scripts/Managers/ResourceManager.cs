using UnityEngine;
using SerializableDictionary.Scripts;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public BuildingManager buildingManager;
    public int wood;
    public int metal;
    public int crystal;
    public int synthia;
    public int workforce;

    [SerializeField] SerializableDictionary<string, Cost> BuidingCostTable = new SerializableDictionary<string, Cost>();

    public UnityEvent OnResourceUpdate;

    [field: SerializeField]
    public int WoodToAdd { get; set; }
    [field: SerializeField]
    public int MetalToAdd { get; set; }
    [field: SerializeField]
    public int SynthiaToAdd { get; set; }
    [field: SerializeField]
    public int CrystalToAdd { get; set; }
    [field: SerializeField]
    public List<int> WorkforceToAdd { get; set; }

    private void Start()
    {
        metal = 200;
        wood = 200; 
        workforce = 10;
        crystal = 200;
        synthia = 100;

        WorkforceToAdd = new List<int>();
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

        double adjuster = 1 + (buildingManager.UniBuldingCoefficient/100);
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
        double adjuster = 1 + (mod / 100);
        if(resource == "Uni")
        {
            WoodToAdd = (int)(WoodToAdd * adjuster);
            MetalToAdd = (int)(MetalToAdd * adjuster);
            CrystalToAdd = (int)(CrystalToAdd * adjuster);
            SynthiaToAdd = (int)(SynthiaToAdd * adjuster);
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
