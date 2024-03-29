using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{

    [SerializeField] GameObject[] screens;
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

    public void OpenHoverDisplayPanel()
    {
        OpenScreen(screens[0]);
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