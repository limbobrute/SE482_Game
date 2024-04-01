using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMain : MonoBehaviour
{
    [SerializeField] BuildingDataNew buildingData; // Reference to the BuildingData asset
    UIHoverData hoverData; 
    GameObject currentBuildingPrefab; // Reference to the instantiated prefab

    // Start is called before the first frame update
    void Start()
    {
        hoverData = new UIHoverData();
        ConstructBuilding();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpgradeBuilding(buildingData.nextLevel);
        }
    }

    // Called when constructing or upgrading the building
    public void ConstructBuilding()
    {
        currentBuildingPrefab = Instantiate(buildingData.buildingPrefab);
        Vector3 originalPosition = currentBuildingPrefab.transform.position;
        Quaternion originalRotation = currentBuildingPrefab.transform.rotation;

        currentBuildingPrefab.transform.SetParent(transform); // Set the parent
        currentBuildingPrefab.transform.localPosition = originalPosition; // Example position
        currentBuildingPrefab.transform.localRotation = originalRotation;

        UpdateHoverData();
    }

    // Called when upgrading the building
    public void UpgradeBuilding(BuildingDataNew nextLevel)
    {
        if (nextLevel != null)
        {
            buildingData = nextLevel;
            Destroy(currentBuildingPrefab); // Destroy the old prefab
            ConstructBuilding(); // Instantiate the new prefab
        }
        else
        {
            Debug.Log("Building Maxed!");
        }
    }

    void UpdateHoverData()
    {
        hoverData.objectName = buildingData.buildingName;
        hoverData.isBuilding = true;
        hoverData.level = buildingData.buildingLevel;
        hoverData.description = buildingData.description;
        hoverData.needRequirementPanel = true;
        hoverData.woodCost = buildingData.woodCost;
        hoverData.crystalCost = buildingData.crystalCost;
        hoverData.metalCost = buildingData.metalCost;
        hoverData.synthiaCost = buildingData.synthiaCost;
        hoverData.manpowerCost = buildingData.builderCost;
        hoverData.constructionTime = buildingData.constructionTime;
    }

    // Update UI with building information
    public UIHoverData GetHoverData()
    {
        // Example: Update UI text labels with building name, level, etc.
        return hoverData;
    }
}
