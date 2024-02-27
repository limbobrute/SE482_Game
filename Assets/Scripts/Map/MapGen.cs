using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;

//Created On: 2/19/2024
//Created By: William HP.
//Last Edited: 2/19/2024
//Edited by: William HP.
//Credit goes to Game Dev Guide, for the LayoutGrid() function, who posted the video showing this function at the following link: https://www.youtube.com/watch?v=EPaSmQ2vtek
public class MapGen : MonoBehaviour
{
    public static MapGen instance;
    private List<GameObject> MadeTiles = new List<GameObject>();
    private int seed = 0;
    private System.Random rand;
    [Header("Tile Settings")]
    public float outersize = 1f;
    public float innersize = 0f;
    public float height = .1f;
    public Material material;

    [Header("Grid Settings")]
    public Vector2Int size;
    public TextMeshProUGUI SeedString;
    public List<GameObject> Tiles = new List<GameObject>();
    [SerializeField]public GameObject[,] EmptyTiles;


    private void Awake()
    {
        if(instance != null && instance != this)
        { Destroy(this); }
        else
        { instance = this; }
    }
    private void Start()
    {
        LayoutGrid();
    }

    #region CREATE_HEX_GRID
    void LayoutGrid()
    {
        GameObject[,] LTile = new GameObject[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                GameObject tile = new GameObject("Hex "+ x.ToString()+","+y.ToString(), typeof(HexRender));
                tile.transform.position = GetPosForHex(new Vector2Int(x, y));
                HexRender hex = tile.GetComponent<HexRender>();
                hex.acceptableTiles = Tiles;
                hex.outerSize = outersize;
                hex.innerSize = innersize;
                hex.height = height;
                hex.material = material;
                hex.x = x;//These two lines of code are for the Wave function Collapse methods
                hex.y = y;
                hex.DrawMesh();

                tile.gameObject.transform.parent = this.gameObject.transform;
                LTile[x, y] = tile;
                
            }
        }
        EmptyTiles = LTile;
    }

    public Vector3 GetPosForHex(Vector2Int coordinate)
    {
        int col = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPos;
        float yPos;
        bool shouldOffset;
        float horiDistance;
        float vertDistance;
        float offset;
        float size = outersize;

        shouldOffset = (col % 2) == 0;
        width = 2f * size;
        height = Mathf.Sqrt(3) * size;

        horiDistance = width * (3f / 4f);
        vertDistance = height;

        offset = (shouldOffset) ? height / 2 : 0;
        xPos = (col * horiDistance);
        yPos = (row * vertDistance) - offset;

        return new Vector3(xPos, 0, -yPos);
    }
    #endregion

    #region PUSDEO_WAVE_FUNCTION_COLLAPSE
    /*public void StartTile()
    {
        var s = SeedString.text.Substring(0, SeedString.text.Length - 1);
        //seed = Convert.ToInt32(SeedString.text.ToString());
        int.TryParse(s, out seed);
        Debug.Log("Seed value is " + seed);
        System.Random Srand = new System.Random(seed);
        rand = Srand;
        int Lrand = rand.Next();
        double Frand = rand.NextDouble();
        Debug.Log("Lrand value is " + Lrand);

        foreach(GameObject tile in EmptyTiles)
        { tile.GetComponent<HexRender>().GetNeigbhours(); }
        GameObject temp = null;
        //int rand = Random.Range(0, EmptyTiles.Count - 1);

        
        var start = Lrand % EmptyTiles.Count;
        temp = EmptyTiles[start];
        //Debug.Log("Collapsing tile: " + temp.name);

        var oldTile = temp.GetComponent<HexRender>(); 
        int randT = (Lrand % Tiles.Count);
        GameObject newTile = Instantiate(Tiles[randT], temp.transform.position, Quaternion.Euler(-90,0,0));
        var range = newTile.GetComponent<Tile>();
        newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y, -Mathf.Lerp(range.LowerRange, range.UpperRange, (float)Frand));

        range.Eneigbhours.Up = oldTile.neigbhours.Up;
        range.Eneigbhours.Down = oldTile.neigbhours.Down;
        range.Eneigbhours.TopLeft = oldTile.neigbhours.TopLeft;
        range.Eneigbhours.TopRight = oldTile.neigbhours.TopRight;
        range.Eneigbhours.BottomLeft = oldTile.neigbhours.BottomLeft;
        range.Eneigbhours.BottomRight = oldTile.neigbhours.BottomRight;
        range.tile = newTile;
        oldTile.entropy.Collapsed = true;

        oldTile.gameObject.SetActive(false);
        newTile.transform.parent = this.gameObject.transform;
        MadeTiles.Add(newTile);
        range.UpdateNeigbhours();
    }

    public GameObject SortGrid()
    {
        List<GameObject> list = new List<GameObject>();
        
        foreach(GameObject tile in EmptyTiles)
        {
            if(!tile.GetComponent<HexRender>().entropy.Collapsed)
            { list.Add(tile); }
        }

        var array = list.ToArray();
        var ret = array[0];

        for(int i = 1; i < array.Length - 1; i++)
        {
            var go = array[i];
            if(ret.GetComponent<HexRender>().acceptableTiles.Count < go.GetComponent<HexRender>().acceptableTiles.Count)
            { continue; }
            else
            { ret = go; }
        }
        //Debug.Log("Next tile to collapse is " + ret.name);
        return ret;
    }

    public void Restart()
    {
        foreach (GameObject tile in EmptyTiles)
        { 
            tile.gameObject.SetActive(true);
            var temp = tile.GetComponent<HexRender>();
            temp.entropy.Collapsed = false;
            temp.acceptableTiles = Tiles;
            temp.GameTiles.Clear();
            temp.entropy.LowerAcceptable = 0;
            temp.entropy.UpperAcceptable = 0;
        }

        foreach (GameObject tile in MadeTiles)
        { Destroy(tile); }
        MadeTiles.Clear();

        StartTile();
    }

    public void NextTile(GameObject tile)
    {
        StartCoroutine(PickTile(tile));
    }

    IEnumerator PickTile(GameObject tile)
    {
        int Lrand = rand.Next(); ;
        double Frand = rand.NextDouble();
        var PossilbleTiles = tile.GetComponent<HexRender>();
        //double Drand = System.Math.Round(Frand, 2);
        Debug.Log("Lrand value is " + Lrand);
        yield return null;

        int LIrand = (Lrand % PossilbleTiles.acceptableTiles.Count);
        var oldTile = tile.GetComponent<HexRender>();
        GameObject newTile = Instantiate(PossilbleTiles.acceptableTiles[LIrand], tile.transform.position, Quaternion.Euler(-90, 0, 0));
        var range = newTile.GetComponent<Tile>();
        newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y, -Mathf.Lerp(range.LowerRange, range.UpperRange, (float)Frand));

        range.Eneigbhours.Up = oldTile.neigbhours.Up;
        range.Eneigbhours.Down = oldTile.neigbhours.Down;
        range.Eneigbhours.TopLeft = oldTile.neigbhours.TopLeft;
        range.Eneigbhours.TopRight = oldTile.neigbhours.TopRight;
        range.Eneigbhours.BottomLeft = oldTile.neigbhours.BottomLeft;
        range.Eneigbhours.BottomRight = oldTile.neigbhours.BottomRight;
        range.tile = newTile;
        oldTile.entropy.Collapsed = true;

        oldTile.gameObject.SetActive(false);
        newTile.transform.parent = this.gameObject.transform;
        MadeTiles.Add(newTile);
        range.UpdateNeigbhours();
        StopCoroutine(PickTile(null));
    }*/
    #endregion

    public void Begin()
    {
        var s = SeedString.text.Substring(0, SeedString.text.Length - 1);
        int.TryParse(s, out seed);
        Debug.Log("Seed is " + s);
        System.Random Srand = new System.Random(seed);
        rand = Srand;

        StartCoroutine(PerlinNoise());

    }

    public void Restart()
    {
        foreach (GameObject tile in EmptyTiles)
        {
            tile.gameObject.SetActive(true);
            /*var temp = tile.GetComponent<HexRender>();
            temp.entropy.Collapsed = false;
            temp.acceptableTiles = Tiles;
            temp.GameTiles.Clear();
            temp.entropy.LowerAcceptable = 0;
            temp.entropy.UpperAcceptable = 0;*/
        }

        foreach (GameObject tile in MadeTiles)
        { Destroy(tile); }
        MadeTiles.Clear();

        var s = SeedString.text.Substring(0, SeedString.text.Length - 1);
        int.TryParse(s, out seed);
        //Debug.Log("Seed is " + s);
        System.Random Srand = new System.Random(seed);
        rand = Srand;

        StartCoroutine(PerlinNoise());
    }


        IEnumerator PerlinNoise()
    {
        for (int x = 0; x < size.x; x++)
        {
            for(int y = 0 ; y < size.y; y++)
            {
                double offX = rand.NextDouble();
                double offY = rand.NextDouble();

                GameObject OldTile = EmptyTiles[x, y];
                float xCoord = (float)x / size.x * 50f + (float)offX;
                float yCoord = (float)y / size.y * 50f + (float)offY;

                //Debug.Log("xCoord is " + xCoord + "\n yCord is " + yCoord);
                //Debug.Log("Perlin noise value is " + Mathf.PerlinNoise(xCoord, yCoord));
                float height = Mathf.Lerp(0, 1, Mathf.PerlinNoise(xCoord, yCoord));
                //Debug.Log("Height value is " + height);
                float Zheight = Mathf.Lerp(0, 25, height);
                foreach(GameObject tile in Tiles)
                {
                    var data = tile.GetComponent<Tile>();
                    if (Zheight >= data.LowerRange && Zheight <= data.UpperRange)
                    {
                        var newTile = Instantiate(tile, OldTile.transform.position, Quaternion.Euler(90, 0, 0));
                        newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y, Zheight);
                        OldTile.SetActive(false);
                        MadeTiles.Add(newTile);
                        break;
                    }
                }
            }
            yield return null;
        }
    }
}