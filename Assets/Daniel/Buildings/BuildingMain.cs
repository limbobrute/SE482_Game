using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMain : MonoBehaviour
{
    [SerializeField] BuildingDataNew buildingData; // Reference to the BuildingData asset
    ObjectDataForUI uiData; 
    GameObject currentBuildingPrefab; // Reference to the instantiated prefab

    // Start is called before the first frame update
    void Start()
    {
        uiData = new ObjectDataForUI();
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

        UpdateUIData();
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

    void UpdateUIData()
    {
        uiData.objectName = buildingData.buildingName;
        uiData.isBuilding = true;
        uiData.level = buildingData.buildingLevel;
        uiData.description = buildingData.description;
        uiData.needRequirementPanel = true;
        uiData.woodCost = buildingData.cost.Wood;
        uiData.crystalCost = buildingData.cost.Crystal;
        uiData.metalCost = buildingData.cost.Metal;
        uiData.synthiaCost = buildingData.cost.Synthia;
        uiData.manpowerCost = buildingData.cost.Workforce;
        uiData.constructionTime = buildingData.cost.Time;
    }

    // Update UI with building information
    public ObjectDataForUI GetUIData()
    {
        // Example: Update UI text labels with building name, level, etc.
        return uiData;
    }
}
