using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;
using System.Threading;
using UnityEngine.Events;

//Created On: 2/19/2024
//Created By: William HP.
//Last Edited: 2/19/2024
//Edited by: William HP.
//Credit goes to Game Dev Guide, for the LayoutGrid() function, who posted the video showing this function at the following link: https://www.youtube.com/watch?v=EPaSmQ2vtek
public class MapGen : MonoBehaviour
{
    public static MapGen instance;
    GameObject[,] MadeTiles;
    private int seed = 0;
    private System.Random rand;
    [Header("Tile Settings")]
    public float outersize = 1f;
    public float innersize = 0f;
    public float height = .1f;
    public Material material;

    [Header("Grid Settings")]
    public bool ChangeSmoothing;
    public Vector2Int size;
    public TextMeshProUGUI SeedString;
    public GameObject StartTile;
    public List<GameObject> Tiles = new List<GameObject>();
    [SerializeField]public GameObject[,] EmptyTiles;

    public UnityEvent OnMapGenComplete;
    

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
        foreach (GameObject tile in EmptyTiles)
        { tile.GetComponent<HexRender>().GetNeigbhours(); }

        var s = SeedString.text.Substring(0, SeedString.text.Length - 1);
        int.TryParse(s, out seed);
        //Debug.Log("Seed is " + s);
        System.Random Srand = new System.Random(seed);
        rand = Srand;
        var temp = new GameObject[size.x, size.y];
        MadeTiles = temp;
        double offX = rand.NextDouble();
        double offY = rand.NextDouble();

        StartCoroutine(PerlinNoise(offX, offY));

    }

    public void BoolChange()
    { ChangeSmoothing = !ChangeSmoothing; }
    public void Restart()
    {
        foreach (GameObject tile in EmptyTiles)
        {
            tile.GetComponent<HexRender>().NewTile = null;
        }

        foreach (GameObject tile in MadeTiles)
        { Destroy(tile); }

        var s = SeedString.text.Substring(0, SeedString.text.Length - 1);
        int.TryParse(s, out seed);
        //Debug.Log("Seed is " + s);
        System.Random Srand = new System.Random(seed);
        rand = Srand;
        double offX = rand.NextDouble();
        double offY = rand.NextDouble();

        StartCoroutine(PerlinNoise(offX, offY));
    }

    void SetStart()
    {
        GameObject tile = MadeTiles[0, 0];
        var oldTile = EmptyTiles[0, 0];
        /*Generate a Start tile at bottom right corner of the map*/
        var startTile = Instantiate(StartTile, tile.transform.position, Quaternion.Euler(90, 0, 0));
        MadeTiles[0, 0] = startTile;
        oldTile.GetComponent<HexRender>().NewTile = startTile;
        Destroy(tile);


        tile = MadeTiles[0, size.y - 1];
        oldTile = EmptyTiles[0, size.y - 1];
        /*Generate a Start tile at the upper right corner of the map*/
        startTile = Instantiate(StartTile, tile.transform.position, Quaternion.Euler(90, 0, 0));
        MadeTiles[0, size.y - 1] = startTile;
        oldTile.GetComponent<HexRender>().NewTile = startTile;
        Destroy(tile);


        tile = MadeTiles[size.x - 1, size.y - 1];
        oldTile = EmptyTiles[size.x - 1, size.y - 1];
        /*Generate a Start tile at the upper left corner of the map*/
        startTile = Instantiate(StartTile, tile.transform.position, Quaternion.Euler(90, 0, 0));
        MadeTiles[size.x - 1, size.y - 1] = startTile;
        oldTile.GetComponent<HexRender>().NewTile = startTile;
        Destroy(tile);


        tile = MadeTiles[size.x - 1, 0];
        oldTile = EmptyTiles[size.x - 1, 0];
        /*Generate a Start tile at the bottom right of the map*/
        startTile = Instantiate(StartTile, tile.transform.position, Quaternion.Euler(90, 0, 0));
        MadeTiles[size.x - 1, 0] = startTile;
        oldTile.GetComponent<HexRender>().NewTile = startTile;
        Destroy(tile);

        OnMapGenComplete?.Invoke();

    }


    IEnumerator PerlinNoise(double offX, double offY)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0 ; y < size.y; y++)
            {
                GameObject newTile = null;
                /*double offX = rand.NextDouble();
                double offY = rand.NextDouble();*/

                GameObject OldTile = EmptyTiles[x, y];
                float xCoord = (float)x / size.x * 50f + (float)offX;
                float yCoord = (float)y / size.y * 50f + (float)offY;

               // Debug.Log("xCoord is " + xCoord + "\n yCord is " + yCoord);
                //Debug.Log("Perlin noise value is " + Mathf.PerlinNoise(xCoord, yCoord));
                //float height = Mathf.Lerp(0, 1, Mathf.PerlinNoise(xCoord, yCoord));0
                //Debug.Log("Height value is " + height);
                float Zheight = Mathf.Lerp(0, 25, Mathf.PerlinNoise(xCoord, yCoord));
                foreach(GameObject tile in Tiles)
                {
                    var data = tile.GetComponent<Tile>();
                    if (Zheight >= data.LowerRange && Zheight <= data.UpperRange)
                    {
                        newTile = Instantiate(tile, OldTile.transform.position, Quaternion.Euler(90, 0, 0));
                        //newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y, Zheight);
                        OldTile.GetComponent<HexRender>().NewTile = newTile;
                        OldTile.GetComponent<MeshRenderer>().enabled = false;
                        MadeTiles[x,y] = newTile;
                        break;
                    }
                }
                if(newTile == null)// I have no clue as to how this can happen, but sometimes it won't make a tile, so we'll force it
                {
                    newTile = Instantiate(Tiles[2], OldTile.transform.position, Quaternion.Euler(90, 0, 0));
                    //newTile.transform.localScale = new Vector3(newTile.transform.localScale.x, newTile.transform.localScale.y, 7f);
                    OldTile.GetComponent<HexRender>().NewTile = newTile;
                    OldTile.GetComponent<MeshRenderer>().enabled = false;
                    MadeTiles[x, y] = newTile;
                }
            }
            yield return null;
        }
        if (MadeTiles[size.x - 1, size.y - 1] != null)
        {
            SetStart();
            /*if (!ChangeSmoothing)
            { StartCoroutine(Smoothing()); }
            else
            { StartCoroutine(Smoothing2()); }
            StopCoroutine(PerlinNoise(offX, offY));*/
        }
    }

    /*IEnumerator Smoothing2()
    {
        foreach(GameObject tile in MadeTiles)
        {
            if (tile.CompareTag("Sand"))
            { tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y, 4.5f); }
            else if (tile.CompareTag("Grass"))
            { tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y, 8.5f); }
            else if (tile.CompareTag("Forest"))
            { tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y, 15f); }
            else if (tile.CompareTag("Crystal"))
            { tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y, 18.5f); }
            else if (tile.CompareTag("Mountain"))
            { tile.transform.localScale = new Vector3(tile.transform.localScale.x, tile.transform.localScale.y, 22f); }
            else if(tile.name == "StartTile(Clone)")
            { continue; }

            yield return null;
        }
    }
    IEnumerator Smoothing()
    {
        foreach(GameObject tile in EmptyTiles)
        {
            //Debug.Log("Smoothing tile at " + tile.name);
            float aveheight = 0f;
            int divider = 0;
            var neigbhours = tile.GetComponent<HexRender>();
            foreach(GameObject NTile in neigbhours.neigbhours)
            {
                var check = NTile.GetComponent<HexRender>().NewTile;
                if(check == null)
                {
                    var whoops = Instantiate(Tiles[2], NTile.transform.position, Quaternion.Euler(90, 0, 0));
                    whoops.transform.localScale = new Vector3(whoops.transform.localScale.x, whoops.transform.localScale.y, 7f);
                    NTile.GetComponent<HexRender>().NewTile = whoops;
                }
                var temp = NTile.GetComponent<HexRender>();
                aveheight += temp.NewTile.transform.localScale.z;
                divider++;
            }
            
            aveheight /= divider;
            neigbhours.NewTile.transform.localScale = new Vector3(neigbhours.NewTile.transform.localScale.x, neigbhours.NewTile.transform.localScale.y, aveheight);
            yield return null;
        }
    }*/
}
