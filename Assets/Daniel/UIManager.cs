using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    HUD,
    TechTree,
}

public class UIManager : MonoBehaviour
{
    public GameObject[] screens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenHUDScreen()
    {
        OpenScreen(ScreenType.HUD);
    }

    public void OpenTechTreeScreen()
    {
        OpenScreen(ScreenType.TechTree);
    }

    void CloseScreens()
    {
        for(int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
    }

    void OpenScreen(ScreenType screen)
    {
        int index = (int)screen;
        if (index >= 0 && index < screens.Length)
        {
            CloseScreens();
            screens[index].SetActive(true);
        }
    }

}
