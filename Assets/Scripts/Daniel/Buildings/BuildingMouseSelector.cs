using UnityEngine;

public class BuildingMouseSelector : MonoBehaviour
{
    [SerializeField] BuildingMain myBuilding;
    [SerializeField] UIManager uiManager;
    [SerializeField] bool canHover = true; // Flag to track hover state
    [SerializeField] bool canSelect = true;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        myBuilding = GetComponent<BuildingMain>();
    }

    private void Start()
    {
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
            uiManager.OpenHoverDisplayPanel(myBuilding.GetThisUIData()); // Show your hover information panel
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
            uiManager.OpenBuildingPopup(myBuilding.GetThisUIData(), myBuilding.GetNextUIData());
            BuildingFlagSet(!uiManager.GetPopupOpen());
        }
    }
}
