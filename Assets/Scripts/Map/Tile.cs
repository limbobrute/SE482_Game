using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private MapGen map;
    private List<GameObject> madeModels = new List<GameObject>();
    public string tileType;
    public List<BuildingDataNew> possibleBuildings;
    public GameObject ModelPlacer;
    public GameObject[] models;
    public int NumberOfModels = 3;

    /*[Serializable]
    public struct EmptyNeigbours
    {
        public GameObject Up;
        public GameObject Down;
        public GameObject TopLeft;
        public GameObject TopRight;
        public GameObject BottomLeft;
        public GameObject BottomRight;

    }
    public EmptyNeigbours Eneigbhours;*/
    [Serializable] public struct XBand
    {
        public float UpperX; 
        public float LowerX;
    }
    [Serializable] public struct YBand
    {
        public float UpperY;
        public float LowerY;
    }
    [Serializable] public struct ZBand
    {
        public float UpperZ;
        public float LowerZ;
    }
    [Tooltip("Range for a model's x transform.position value")]public XBand xband;
    [Tooltip("Range for a model's y transform.position value")] public YBand yband;
    [Tooltip("Range for a model's z transform.position value")] public ZBand zband;
    [Tooltip("Upper Z scale value that this tile can be found at.")]
    public float UpperRange = 0f;
    [Tooltip("Lower Z scale value that this tile can be found at.")]
    public float LowerRange = 0f;
    public float AcceptableRange = 0f;
    //public GameObject tile;

    private void Awake()
    {
        map = MapGen.instance;
    }

    private void Start()
    {
        
        if (NumberOfModels > 0)
        {
            for (int i = 0; i < NumberOfModels; i++)
            {
                int rand = 0;
                if (models.Length > 1)
                { rand = UnityEngine.Random.Range(0, models.Length - 1); }
                GameObject model = Instantiate(models[rand], transform.position = ModelPlacer.transform.position, Quaternion.identity);
                model.transform.parent = this.gameObject.transform;
                Vector3 localLocation = new Vector3(UnityEngine.Random.Range(xband.LowerX, xband.UpperX), UnityEngine.Random.Range(yband.LowerY, yband.UpperY), UnityEngine.Random.Range(zband.LowerZ, zband.UpperZ));
                model.transform.localPosition = localLocation;
                float xRotate = UnityEngine.Random.Range(0f, 360f);
                //model.transform.localRotation = Quaternion.Euler(xRotate, model.transform.localRotation.y, model.transform.localRotation.z);
                //model.transform.parent = null;
                //model.transform.localScale = new Vector3(0.01f, 0.1f, (this.gameObject.transform.localScale.z/1000));
                madeModels.Add(model);
            }
        }
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        foreach(GameObject model in madeModels)
        { 
            model.transform.parent = null;
            float yRotate = UnityEngine.Random.Range(0f, 360f);
            model.transform.localRotation = Quaternion.Euler(model.transform.localRotation.x, yRotate, model.transform.localRotation.z);
        }
    }

    /*public void UpdateNeigbhours()
    {
        //Debug.Log("Name of this tile is " + gameObject.name);
        if(Eneigbhours.Up != null)
        { Eneigbhours.Up.GetComponent<HexRender>().ReduceEntropy(gameObject); }

        if (Eneigbhours.Down != null)
        { Eneigbhours.Down.GetComponent<HexRender>().ReduceEntropy(gameObject); }

        if (Eneigbhours.TopLeft != null)
        { Eneigbhours.TopLeft.GetComponent<HexRender>().ReduceEntropy(gameObject); }

        if (Eneigbhours.TopRight != null)
        { Eneigbhours.TopRight.GetComponent<HexRender>().ReduceEntropy(gameObject); }

        if (Eneigbhours.BottomLeft != null)
        { Eneigbhours.BottomLeft.GetComponent<HexRender>().ReduceEntropy(gameObject); }

        if (Eneigbhours.BottomRight != null)
        { Eneigbhours.BottomRight.GetComponent<HexRender>().ReduceEntropy(gameObject); }

        var next = map.SortGrid();
        map.NextTile(next);

    }*/

}
