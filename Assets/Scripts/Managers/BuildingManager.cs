using UnityEngine;
using System.Collections.Generic;
using Enums;
using UnityEngine.Events;
using System;

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
            // Instantiate the BuildingMain prefab
            GameObject newBuilding = Instantiate(buildingData.buildingPrefab, instancePostition, Quaternion.identity);
            newBuilding.AddComponent<BuildingMain>();
            newBuilding.GetComponent<BuildingMain>().buildingData = buildingData;
            newBuilding.GetComponent<BuildingMain>().UpdateUIData();
            newBuilding.AddComponent<BuildingMouseSelector>();
            newBuilding.AddComponent<BoxCollider>();

            BuildingsInScene.Add(newBuilding.GetComponent<BuildingMain>());
            ResourceManager.DeductResource(buildingData.cost);
            ResourceManager.printResources();
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
}

