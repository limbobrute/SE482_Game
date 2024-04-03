using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelecter : MonoBehaviour
{
    BuildingManager buildingManager;
    UIManager uiManager;
    ObjectDataForUI[] uiData = new ObjectDataForUI[4];
    BuildingDataNew[] buildingDatas = new BuildingDataNew[4];
    private Renderer render;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        uiManager = FindObjectOfType<UIManager>();

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

    private void OnMouseDown()
    {
        if (uiManager != null && !uiManager.GetPopupOpen())
        {
            if (buildingManager != null)
            {
                float height = render.bounds.size.y;
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
                buildingManager.instancePostition = newPosition;
            }
            List<BuildingDataNew> buildings = GetComponent<Tile>().possibleBuildings;
            int i = 0;
            foreach (var building in buildings)
            {
                uiData[i] = new ObjectDataForUI();
                UpdateUIDataInfo(uiData[i], building);
                i++;
            }
            uiManager.OpenTilePopup(GetComponent<Tile>().tileType, uiData, buildings, buildings.Count);
        }
    }
    void UpdateUIDataInfo(ObjectDataForUI od, BuildingDataNew bd)
    {
        od.objectName = bd.buildingName;
        od.isBuilding = true;
        od.level = bd.buildingLevel.ToString();
        od.description = bd.description;
        // Need to further work buff
        od.buff = "+" + bd.description;
        od.needRequirementPanel = true;
        od.woodCost = bd.cost.Wood.ToString();
        od.crystalCost = bd.cost.Crystal.ToString();
        od.metalCost = bd.cost.Metal.ToString();
        od.synthiaCost = bd.cost.Synthia.ToString();
        od.manpowerCost = bd.cost.Workforce.ToString();
        od.constructionTime = bd.cost.Time.ToString();
    }
}
