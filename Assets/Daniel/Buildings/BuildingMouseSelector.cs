using UnityEngine;

public class BuildingMouseSelector : MonoBehaviour
{
    [SerializeField] BuildingMain myBuilding;
    [SerializeField] UIManager uiManager;
    [SerializeField] bool canHover = true; // Flag to track hover state
    [SerializeField] bool canSelect = true;

    private void Start()
    {
        myBuilding = GetComponent<BuildingMain>();
        BuildingFlagSet(!uiManager.GetPopupOpen()); 
    }
    void BuildingFlagSet(bool setting)
    {
        canHover = setting;
        canSelect = setting;
    }

    private void OnMouseEnter()
    {
        BuildingFlagSet(!uiManager.GetPopupOpen());
        if (canHover)
        {
            uiManager.OpenHoverDisplayPanel(myBuilding.GetHoverData()); // Show your hover information panel
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
            uiManager.OpenBuildingPopup();
            BuildingFlagSet(!uiManager.GetPopupOpen());
        }
    }
}
