using UnityEngine;
using System.Collections.Generic;
using Enums;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    [field: SerializeField]
    public ResourceManager ResourceManager { get; set; }
    [field: SerializeField]
    public List<BuildingMain> BuildingTypesToBuild { get; set; }

    [field: SerializeField]
    public List<BuildingMain> BuildingsInScene { get; set; }

    public Vector3 instancePostition = Vector3.zero;
    public UnityEvent onNotEnoughResources, onResearchBuilt, onResearchUpgrade;
    public GameObject scaffoldingPrefab;

    //These values are to help track any/all changes to building cost
    [HideInInspector]public int UniBuldingCoefficient = 0;
    [HideInInspector] public int LumberCoefficient = 0;

    private void Start()
    {
        BuildingsInScene = new();
    }
    public void InstantiateBuilding(BuildingDataNew buildingData)
    {
        if (buildingData is null)
        {
            throw new ArgumentNullException(nameof(buildingData));
        }

        if (ResourceManager.CanBuild(buildingData.cost))
        {
            Vector3 instancePositionAtSelection = instancePostition;

            StartCoroutine(DelayedInstantiate(buildingData, instancePositionAtSelection));

        }
        else
        {
            Debug.Log("Not Enough Resources to Build");
            onNotEnoughResources?.Invoke();
        }
    }

    public void UpgradeBuilding(GameObject currentBuilding)
    {
        BuildingDataNew currentBuildingData = currentBuilding.GetComponent<BuildingMain>()?.buildingData;
        if (currentBuildingData is null) { 
            throw new ArgumentNullException(nameof(currentBuildingData));

        }
        if (ResourceManager.CanBuild(currentBuildingData.cost))
        {
            BuildingsInScene.Remove(currentBuilding.GetComponent<BuildingMain>());

            switch (currentBuildingData.buildingName)
            {
                case "Lumbermill":
                    ResourceManager.WoodToAdd = ResourceManager.WoodToAdd - currentBuildingData.flatResourceIncrement;
                    break;
                case "Crystal Mine":
                    ResourceManager.CrystalToAdd = ResourceManager.CrystalToAdd - currentBuildingData.flatResourceIncrement;
                    break;
                case "Research Factory":
                    onResearchUpgrade?.Invoke();
                    break;
                default:
                    Debug.Log("BuildingName Not Recognized");
                    break;
            }
            Vector3 buildingPosition = currentBuilding.transform.position;//the upgraded building should not show up where when a different tile is selected
            Destroy(currentBuilding);
            StartCoroutine(DelayedInstantiate(currentBuildingData.nextLevel, buildingPosition));

        }
        else
        {
            Debug.Log("Not Enough Resources to Build");
            onNotEnoughResources?.Invoke();
        }
    }

    public void IncreaseProductionRate(BuildingType buildingType, int rate)
    {
    }

    public void DecreaseBuildingCosts(int buildingType)
    {
    }

    public void AltarBuildMod(TechTreeNode node)
    {
        var Dic = node.Bonuses;
        string building = null;
        Debug.Log(node.name);
        if(Dic.ContainsKey("Housing"))
        { building = "Housing"; }
        else if(Dic.ContainsKey("Mill"))
        { building = "Mill"; }
        else if (Dic.ContainsKey("Uni"))
        { building = "Uni"; }        
        //else if()//Add additional building logic here

        int bonus = Dic.Get(building);

        if(building == "Mill")
        { LumberCoefficient += bonus; }
        else if(building == "Uni")
        { UniBuldingCoefficient += bonus; }
        //else if()//Add additional building logic here
    }
    IEnumerator DelayedInstantiate(BuildingDataNew buildingData, Vector3 instancePositionAtSelection)
    {
        ResourceManager.DeductResource(buildingData.cost);
        ResourceManager.printResources();

        GameObject scaffolding = Instantiate(scaffoldingPrefab, instancePositionAtSelection, Quaternion.identity);

        // Wait for the specified amount of in-game time
        yield return RTSTimer.Instance.WaitForInGameSeconds(buildingData.cost.Time);

        // Destroy the Scaffolding
        Destroy(scaffolding);
        // Instantiate the BuildingMain prefab
        // ... rest of your code ...

        // Instantiate the BuildingMain prefab
        ResourceManager.ReallocateBuilder(buildingData.cost.Workforce);
        ResourceManager.printResources();
        GameObject newBuilding = Instantiate(buildingData.buildingPrefab, instancePositionAtSelection, Quaternion.identity);
        newBuilding.GetComponent<BuildingMain>().UpdateUIData();

        BuildingsInScene.Add(newBuilding.GetComponent<BuildingMain>());


        // Add the flatResourceIncrement to the corresponding resource in ResourceManager

        switch (buildingData.buildingName)
        {
            case "Lumbermill":
                ResourceManager.WoodToAdd = ResourceManager.WoodToAdd + buildingData.flatResourceIncrement;
                break;
            case "Crystal Mine":
                ResourceManager.CrystalToAdd = ResourceManager.CrystalToAdd + buildingData.flatResourceIncrement;
                break;
            case "Research Factory":
                onResearchBuilt?.Invoke();
                break;
            default:
                Debug.Log("BuildingName Not Recognized");
                break;
        }
    }
}

