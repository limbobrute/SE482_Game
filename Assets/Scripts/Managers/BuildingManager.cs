using UnityEngine;
using System.Collections.Generic;
using Enums;
using UnityEngine.Events;
using System;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    [field: SerializeField]
    public ResourceManager ResourceManager { get; set; }
    [field: SerializeField]
    public List<BuildingMain> BuildingTypesToBuild { get; set; }

    [field: SerializeField]
    public List<BuildingMain> BuildingsInScene { get; set; }

    public Vector3 instancePostition = Vector3.zero;
    public UnityEvent onNotEnoughResources;
    public GameObject scaffoldingPrefab;

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
            StartCoroutine(DelayedInstantiate(buildingData));

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

    IEnumerator DelayedInstantiate(BuildingDataNew buildingData)
    {
        ResourceManager.DeductResource(buildingData.cost);
        ResourceManager.printResources();

        GameObject scaffolding = Instantiate(scaffoldingPrefab, instancePostition, Quaternion.identity);

        // Wait for the specified amount of in-game time
        yield return RTSTimer.Instance.WaitForInGameSeconds(buildingData.cost.Time);

        // Destroy the Scaffolding
        Destroy(scaffolding);
        // Instantiate the BuildingMain prefab
        // ... rest of your code ...

        // Instantiate the BuildingMain prefab
        ResourceManager.ReallocateBuilder(buildingData.cost.Workforce);
        ResourceManager.printResources();
        GameObject newBuilding = Instantiate(buildingData.buildingPrefab, instancePostition, Quaternion.identity);
        newBuilding.AddComponent<BuildingMain>();
        newBuilding.GetComponent<BuildingMain>().buildingData = buildingData;
        newBuilding.GetComponent<BuildingMain>().UpdateUIData();
        newBuilding.AddComponent<BuildingMouseSelector>();
        newBuilding.AddComponent<BoxCollider>();

        BuildingsInScene.Add(newBuilding.GetComponent<BuildingMain>());


        // Add the flatResourceIncrement to the corresponding resource in ResourceManager

        switch (buildingData.buildingName)
        {
            case "Lumbermill":
                ResourceManager.WoodToAdd = ResourceManager.WoodToAdd + buildingData.flatResourceIncrement;
                break;
            default:
                Debug.Log("BuildingName Not Recognized");
                break;
        }
    }
}

