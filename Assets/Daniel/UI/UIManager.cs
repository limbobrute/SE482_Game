using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject[] screens;
    [SerializeField] GameObject hoverRequirementPanel;
    [SerializeField] Text[] hoverTexts;
    [SerializeField] Text[] buildingPopupTexts;
    bool popupOpen = false;

    private void Start()
    {
        InitializeScreen();
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
        for (int i = 1; i < screens.Length; i++)
        {
            CloseScreen(screens[i]);
        }
        popupOpen = false;
    }

    public bool GetPopupOpen()
    {
        return popupOpen;
    }

    public void OpenHoverDisplayPanel(ObjectDataForUI objectUIData)
    {
        OpenScreen(screens[1]);
        if (objectUIData.isBuilding)
        {
            hoverTexts[0].text = objectUIData.objectName + " LV " + objectUIData.level.ToString();
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

    public void OpenBuildingPopup(ObjectDataForUI objectUIData)
    {
        CloseAllScreens();
        OpenScreen(screens[2]);
        popupOpen = true;

        buildingPopupTexts[0].text = objectUIData.objectName + " LV " + objectUIData.level.ToString();
        buildingPopupTexts[1].text = objectUIData.woodCost.ToString();
        buildingPopupTexts[2].text = objectUIData.crystalCost.ToString();
        buildingPopupTexts[3].text = objectUIData.metalCost.ToString();
        buildingPopupTexts[4].text = objectUIData.synthiaCost.ToString();
        buildingPopupTexts[5].text = objectUIData.manpowerCost.ToString();
        buildingPopupTexts[6].text = objectUIData.constructionTime.ToString() + "s";
        if(objectUIData.level < 1)
        {
            buildingPopupTexts[7].text = "Buy";
        }
        else
        {
            buildingPopupTexts[7].text = "Upgrade";
        }
    }

    public void CloseBuildingPopup()
    {
        CloseScreen(screens[2]);
        popupOpen = false;
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
}