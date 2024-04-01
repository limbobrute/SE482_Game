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
            Debug.Log("No BuildingManager found in the scene.");
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
        if (buildingManager != null)
        {
            float height = render.bounds.size.y;
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            buildingManager.instancePostition = newPosition;
        }
    }


}
