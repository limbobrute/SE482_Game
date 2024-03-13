using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelecter : MonoBehaviour
{
    BuildingManager buildingManager;
    private Renderer render;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();

        if (GetComponent<Renderer>())
        { render = GetComponent<Renderer>(); color = render.material.color; }

        if (buildingManager == null)
        {
            Debug.LogError("No BuildingManager found in the scene.");
        }
    }

    private void OnMouseEnter()
    {
        render.material.color = Color.blue;
    }

    private void OnMouseExit()
    {
        render.material.color = color;
    }

    private void OnMouseDown() {
        buildingManager.instancePostition = transform.position;
    }


}
