using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenNavigation : MonoBehaviour
{
    public GameObject[] screens;
    public int currIndex = 0;

    public void NextTree()
    {
        int i = currIndex;
        i = (i + 1) % screens.Length;
        screens[currIndex].SetActive(false);
        screens[i].SetActive(true);
        currIndex = i;
    }

    public void PreviousTree()
    {
        int i = currIndex;
        i--;
        if (i < 0) { i = screens.Length - 1; }
        screens[currIndex].SetActive(false);
        screens[i].SetActive(true);
        currIndex = i;
    }
}
