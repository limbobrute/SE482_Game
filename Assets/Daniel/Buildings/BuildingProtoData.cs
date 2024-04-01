using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProtoData : MonoBehaviour
{
    public string tileName;
    public string desription;
    public GameObject[] buildingPrefabs;
    [SerializeField] UIManager uiManager;
    [SerializeField] bool canHover = true; // Flag to track hover state
    [SerializeField] bool canSelect = true;

    private void Start()
    {    // Find the UIManager object in the scene
        GameObject uiManagerObject = GameObject.Find("UIManager");

        // Get the UIManager component
        uiManager = uiManagerObject.GetComponent<UIManager>();

        BuildingFlagSet(!uiManager.GetPopupOpen());
    }


    void BuildingFlagSet(bool setting)
    {
        canHover = setting;
        canSelect = setting;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Ran 2.a");
        BuildingFlagSet(!uiManager.GetPopupOpen());
        if (canHover)
        {
            Debug.Log("Ran 2.b");
            uiManager.OpenHoverDisplayPanel(tileName, desription); // Show your hover information panel
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("Ran 1");
        uiManager.CloseHoverDisplayPanel(); // Show your hover information panel
    }

    private void OnMouseDown()
    {
        if (canSelect)
        {
            //uiManager.OpenBuildingPopup(myBuilding.GetUIData());
            //BuildingFlagSet(!uiManager.GetPopupOpen());
        }
    }
}
