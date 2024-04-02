using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private MapGen map;

    [Serializable]
    public struct EmptyNeigbours
    {
        public GameObject Up;
        public GameObject Down;
        public GameObject TopLeft;
        public GameObject TopRight;
        public GameObject BottomLeft;
        public GameObject BottomRight;

    }
    public EmptyNeigbours Eneigbhours;
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
