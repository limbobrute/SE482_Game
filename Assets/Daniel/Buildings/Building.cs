using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingDataNew buildingData; // Reference to the BuildingData asset

    private GameObject currentBuildingPrefab; // Reference to the instantiated prefab

    // Called when constructing or upgrading the building
    public void ConstructBuilding()
    {
        // Instantiate the prefab
        currentBuildingPrefab = Instantiate(buildingData.buildingPrefab);
        // Make the instantiated prefab a child of the EmptyGameObject
        currentBuildingPrefab.transform.SetParent(transform); // Set the parent
        // Set its position, rotation, and other properties
        currentBuildingPrefab.transform.position = Vector3.zero; // Example position
        currentBuildingPrefab.transform.rotation = Quaternion.identity; // Example rotation

        // Update UI and other relevant logic
        UpdateUI();
    }

    // Called when upgrading the building
    public void UpgradeBuilding(BuildingDataNew nextLevel)
    {
        if (nextLevel != null)
        {
            buildingData = nextLevel;
            Destroy(currentBuildingPrefab); // Destroy the old prefab
            ConstructBuilding(); // Instantiate the new prefab
            //Display();
        }
        else
        {
            Debug.Log("Building Maxed!");
        }
    }

    // Update UI with building information
    void UpdateUI()
    {
        // Example: Update UI text labels with building name, level, etc.
    }

    // Start is called before the first frame update
    void Start()
    {
        ConstructBuilding();
        Display();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //BuildingDataNew nextLevel;
            //nextLevel = buildingData.nextLevel;

            UpgradeBuilding(buildingData.nextLevel);
        }
    }

    void Display()
    {
        // Example: Accessing properties from BuildingData
        Debug.Log($"Building Name: {buildingData.buildingName}");
        Debug.Log($"Building Level: {buildingData.buildingLevel}"); 
        Debug.Log($"Construction Time: {buildingData.constructionTime} seconds");
    }
}
