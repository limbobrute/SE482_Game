using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject[] screens;
    [SerializeField] GameObject hoverRequirementPanel;
    [SerializeField] Text[] hoverTexts;
    bool popupOpen = false;

    private void Start()
    {
        
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
        for (int i = 0; i < screens.Length; i++)
        {
            CloseScreen(screens[i]);
        }
        popupOpen = false;
    }

    public bool GetPopupOpen()
    {
        return popupOpen;
    }

    public void OpenHoverDisplayPanel(UIHoverData objectUIData)
    {
        OpenScreen(screens[0]);
        hoverTexts[0].text = objectUIData.objectName + " LV " + objectUIData.level.ToString();
        hoverTexts[1].text = objectUIData.description;
        if (objectUIData.needRequirementPanel)
        {
            hoverRequirementPanel.SetActive(true);
            hoverTexts[2].text = objectUIData.woodCost.ToString();
            hoverTexts[3].text = objectUIData.crystalCost.ToString();
            hoverTexts[4].text = objectUIData.metalCost.ToString();
            hoverTexts[5].text = objectUIData.synthiaCost.ToString();
            hoverTexts[6].text = objectUIData.manpowerCost.ToString();
            hoverTexts[7].text = objectUIData.constructionTime.ToString() + "s";
        }
        else
        {
            hoverRequirementPanel.SetActive(false);
        }
    }

    // Example usage to close the DisplayPanel
    public void CloseHoverDisplayPanel()
    {
        CloseScreen(screens[0]);
    }

    public void OpenBuildingPopup()
    {
        CloseAllScreens();
        OpenScreen(screens[1]);
        popupOpen = true;
    }

    public void CloseBuildingPopup()
    {
        CloseScreen(screens[1]);
        popupOpen = false;
    }

    public void OpenTechTreePopup()
    {
        CloseAllScreens();
        OpenScreen(screens[2]);
        popupOpen = true;
    }

    public void CloseTechTreePopup()
    {
        CloseScreen(screens[2]);
        popupOpen = false;
    }
}