using UnityEngine;
using System.Collections.Generic;
using Enums;
using System.Resources;

public class BuildingManager : MonoBehaviour
{
    [field: SerializeField]
    public ResourceManager ResourceManager { get; set; }
    [field: SerializeField]
    public List<BuildingMain> BuildingTypesToBuild { get; set; }

    [field: SerializeField]
    public List<BuildingMain> BuildingsInScene { get; set; }

    public Vector3 instancePostition = Vector3.zero;


    private void Start()
    {

        BuildingsInScene = new();
    }
    public void InstantiateBuilding(BuildingDataNew buildingData)
    {
        // Instantiate the BuildingMain prefab
        GameObject newBuilding = Instantiate(buildingData.buildingPrefab);
        newBuilding.AddComponent<BuildingMain>();
        newBuilding.GetComponent<BuildingMain>().buildingData = buildingData;
        newBuilding.GetComponent<BuildingMain>().UpdateUIData();
        newBuilding.AddComponent<BuildingMouseSelector>();
        newBuilding.AddComponent<BoxCollider>();

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

    public void IncreaseProductionRate(BuildingType buildingType, int rate)
    {
    }

    public void DecreaseBuildingCosts(int buildingType)
    {
    }
}

