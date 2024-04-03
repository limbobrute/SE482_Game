using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BuildingMain : MonoBehaviour
    {
        [SerializeField] public BuildingDataNew buildingData; // Reference to the BuildingData asset
        ObjectDataForUI uiData = new ObjectDataForUI();
        ObjectDataForUI nextUIData= new ObjectDataForUI();
        GameObject currentBuildingPrefab; // Reference to the instantiated prefab

        // Start is called before the first frame update
        void Start()
        {
            //uiData = new ObjectDataForUI();
            //nextUIData = new ObjectDataForUI();
            //ConstructBuilding();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                UpgradeBuilding(buildingData.nextLevel);
            }
        }

        // Called when constructing or upgrading the building
        //public void ConstructBuilding()
        //{
        //    currentBuildingPrefab = Instantiate(buildingData.buildingPrefab);
        //    Vector3 originalPosition = currentBuildingPrefab.transform.position;
        //    Quaternion originalRotation = currentBuildingPrefab.transform.rotation;

        //    currentBuildingPrefab.transform.SetParent(transform); // Set the parent
        //    currentBuildingPrefab.transform.localPosition = originalPosition; // Example position
        //    currentBuildingPrefab.transform.localRotation = originalRotation;
        //    currentBuildingPrefab.transform.localScale = new Vector3(1, 1, 1);

        //    UpdateUIData();
        //}

        // Called when upgrading the building
        public void UpgradeBuilding(BuildingDataNew nextLevel)
        {
            if (nextLevel != null)
            {
                buildingData = nextLevel;
                Destroy(currentBuildingPrefab); // Destroy the old prefab
                //ConstructBuilding(); // Instantiate the new prefab
            }
            else
            {
                Debug.Log("Building Maxed!");
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

       public void UpdateUIData()
        {
            UpdateUIDataInfo(uiData, buildingData);
            UpdateUIDataInfo(nextUIData, buildingData.nextLevel);
        }

        // Update UI with building information
        public ObjectDataForUI GetThisUIData()
        {
            // Example: Update UI text labels with building name, level, etc.
            return uiData;
        }

        public ObjectDataForUI GetNextUIData()
        {
            return nextUIData;
        }
    }