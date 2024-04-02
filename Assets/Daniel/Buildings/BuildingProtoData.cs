using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProtoData : MonoBehaviour
{
    public string tileName;
    public string desription;
    public GameObject[] buildingPrefabs;
    public BuildingDataNew[] buildingData;
    ObjectDataForUI[] uiData = new ObjectDataForUI[4];
    [SerializeField] UIManager uiManager;
    [SerializeField] bool canHover = true; // Flag to track hover state
    [SerializeField] bool canSelect = true;

    private void Start()
    {    // Find the UIManager object in the scene
        GameObject uiManagerObject = GameObject.Find("UIManager");

        // Get the UIManager component
        uiManager = uiManagerObject.GetComponent<UIManager>();

        BuildingFlagSet(!uiManager.GetPopupOpen());


        for (int i = 0; i < buildingData.Length; i++)
        {
            uiData[i] = new ObjectDataForUI();
            UpdateUIDataInfo(uiData[i], buildingData[i]);
        }
    }

    void BuildingFlagSet(bool setting)
    {
        canHover = setting;
        canSelect = setting;
    }

    void UpdateUIDataInfo(ObjectDataForUI od, BuildingDataNew bd)
    {
        od.objectName = bd.buildingName;
        od.isBuilding = true;
        od.level = bd.buildingLevel.ToString();
        od.description = bd.description;
        // Need to further work buff
        od.buff = "+" + bd.description;
        od.needRequirementPanel = true;
        od.woodCost = bd.cost.Wood.ToString();
        od.crystalCost = bd.cost.Crystal.ToString();
        od.metalCost = bd.cost.Metal.ToString();
        od.synthiaCost = bd.cost.Synthia.ToString();
        od.manpowerCost = bd.cost.Workforce.ToString();
        od.constructionTime = bd.cost.Time.ToString();
    }

    private void OnMouseEnter()
    {
        BuildingFlagSet(!uiManager.GetPopupOpen());
        if (canHover)
        {
            uiManager.OpenHoverDisplayPanel(tileName, desription); // Show your hover information panel
        }
    }

    private void OnMouseExit()
    {
        uiManager.CloseHoverDisplayPanel(); // Show your hover information panel
    }

    private void OnMouseDown()
    {
        if (canSelect)
        {
            uiManager.OpenTilePopup(tileName, uiData, buildingData.Length);
            BuildingFlagSet(!uiManager.GetPopupOpen());
        }
    }
}
