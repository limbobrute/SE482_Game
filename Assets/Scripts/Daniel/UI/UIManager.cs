using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject[] screens;
    [SerializeField] GameObject hoverRequirementPanel;
    [SerializeField] Text[] hoverTexts;
    [SerializeField] GameObject[] buildingsToBuyPanels;
    [SerializeField] Button[] buildingsToBuyButtons;
    [SerializeField] Button upgradeButton;
    [SerializeField] Text[] tilePopupTexts;
    [SerializeField] Text[] buildingPopupTexts;
    [SerializeField] Button ButtonTech;
    BuildingManager buildingManager;

    [SerializeField] bool popupOpen = false;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        InitializeScreen();
    }

    private void Update()
    {
        // This stops NoResource popup from allowing other windows to activate
        if (screens[5].activeSelf && !popupOpen)
        {
            popupOpen = true;
        }
    }

    private void InitializeScreen()
    {
        CloseAllScreens();
    }

    void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);
    }

    void CloseScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    void CloseAllScreens()
    {
        for(int i = 0; i < buildingsToBuyPanels.Length; i++)
        {
            buildingsToBuyPanels[i].SetActive(false); 
        }
        for (int i = 1; i < screens.Length; i++)
        {
            CloseScreen(screens[i]);
        }
        popupOpen = false;
    }

    public void CloseMapGen()
    {
        CloseScreen(screens[0]);
    }

    public bool GetPopupOpen()
    {
        return popupOpen;
    }

    public void SetPopUpOpen(bool isOpen)
    {
        popupOpen = isOpen;
    }

    public void OpenHoverDisplayPanel(string title, string description)
    {
        OpenScreen(screens[1]);
        hoverRequirementPanel.SetActive(false);
        hoverTexts[0].text = title;
        hoverTexts[1].text = description;
    }

    public void OpenHoverDisplayPanel(ObjectDataForUI objectUIData)
    {
        OpenScreen(screens[1]);
        if (objectUIData.isBuilding)
        {
            hoverTexts[0].text = objectUIData.objectName + " <Lv" + objectUIData.level + ">";
        }
        else
        {
            hoverTexts[0].text = objectUIData.objectName;;
        }

        //Debug.Log(objectUIData.description);
        hoverTexts[1].text = objectUIData.description;
        
        if (objectUIData.needRequirementPanel)
        {
            hoverRequirementPanel.SetActive(true);
            hoverTexts[2].text = objectUIData.woodCost.ToString();
            hoverTexts[3].text = objectUIData.crystalCost.ToString();
            hoverTexts[4].text = objectUIData.metalCost.ToString();
            hoverTexts[5].text = objectUIData.synthiaCost.ToString();
            if (objectUIData.isBuilding)
            {
                hoverTexts[6].text = objectUIData.manpowerCost.ToString();
                hoverTexts[7].text = objectUIData.constructionTime.ToString() + "s";
            }
            else
            {
                hoverTexts[6].text = "N/A";
                hoverTexts[7].text = "N/A";
            }
        }
        else
        {
            hoverRequirementPanel.SetActive(false);
        }
    }

    // Example usage to close the DisplayPanel
    public void CloseHoverDisplayPanel()
    {
        CloseScreen(screens[1]);
    }

    void BuildingDisplay(ObjectDataForUI uiData, Text[] texts, int index)
    {
        if (uiData != null)
        {
            texts[index].text = uiData.objectName + " <Lv" + uiData.level + ">";
            texts[index + 1].text = uiData.description;
            texts[index + 2].text = uiData.woodCost;
            texts[index + 3].text = uiData.crystalCost;
            texts[index + 4].text = uiData.metalCost;
            texts[index + 5].text = uiData.synthiaCost;
            texts[index + 6].text = uiData.manpowerCost;
            texts[index + 7].text = uiData.constructionTime + "s";
       
        }
        else
        {
            texts[index].text = "";
            texts[index + 2].text = "";
            texts[index + 3].text = "MAXED OUT";
            texts[index + 4].text = "";
            texts[index + 5].text = "";
            texts[index + 6].text = "";
            texts[index + 7].text = "";
            texts[index + 8].text = "";
        }
    }

    public void OpenTilePopup(string name, ObjectDataForUI[] uiData, List<BuildingDataNew> buildingData, int length)
    {
        CloseAllScreens();
        OpenScreen(screens[4]);
        popupOpen = true;

        tilePopupTexts[0].text = name;
        for (int i = 0; i < length; i++)
        {
            int index = (i * 8) + 1;
            buildingsToBuyPanels[i].SetActive(true);

            // Create a temporary variable to hold the current value of i
            int tempI = i;
            buildingsToBuyButtons[i].onClick.AddListener(() => buildingManager.InstantiateBuilding(buildingData[tempI]));
            buildingsToBuyButtons[i].onClick.AddListener(() => CloseTilePopup());


            BuildingDisplay(uiData[i], tilePopupTexts, index);
        }
    }

    public void CloseTilePopup()
    {
        for (int i = 0; i < buildingsToBuyPanels.Length; i++)
        {
            // Remove all listeners from the button
            buildingsToBuyButtons[i].onClick.RemoveAllListeners();

            buildingsToBuyPanels[i].SetActive(false);
        }

        CloseScreen(screens[4]);
        popupOpen = false;
    }

    public void OpenBuildingPopup(ObjectDataForUI currUIData, ObjectDataForUI nextUIData, GameObject currentBuilding)
    {
        CloseAllScreens();
        OpenScreen(screens[2]);
        popupOpen = true;

        buildingPopupTexts[0].text = currUIData.objectName; 
        buildingPopupTexts[1].text = currUIData.objectName + " <Lv" + currUIData.level + ">";
        buildingPopupTexts[2].text = currUIData.description;
        BuildingDisplay(nextUIData, buildingPopupTexts, 3);

        upgradeButton.onClick.AddListener(() => buildingManager.UpgradeBuilding(currentBuilding));
        upgradeButton.onClick.AddListener(() => CloseBuildingPopup());
    }

    public void CloseBuildingPopup()
    {
        upgradeButton.onClick.RemoveAllListeners();
        CloseScreen(screens[2]);
        popupOpen = false;
    }

    public void EnableTechButton()
    {
        if(ButtonTech.interactable == false)
        {
            ButtonTech.interactable = true;
        }        
    }

    public void OpenTechTreePopup()
    {
        CloseAllScreens();
        OpenScreen(screens[3]);
        popupOpen = true;
    }

    public void CloseTechTreePopup()
    {
        CloseScreen(screens[3]);
        popupOpen = false;
    }

    public void OpenNotEnoughResourcesPopup()
    {
        CloseAllScreens();
        OpenScreen(screens[5]);
        popupOpen = true;
    }

    public void CloseNotEnoughResourcesPopup()
    {
        CloseScreen(screens[5]);
        popupOpen = false;
    }

    public void OpenPauseMenuPopup()
    {
        CloseAllScreens();
        OpenScreen(screens[6]);
        popupOpen = true;
    }

    public void ClosePauseMenuPopup()
    {
        CloseScreen(screens[6]);
        popupOpen = false;
    }
}