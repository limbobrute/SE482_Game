using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created On: 2/19/2024
//Created By: William HP.
//Last Edited: 2/19/2024
//Edited by: William HP.
//Credit goes to Game Dev Guide, who posted the video showing this, at the following link: https://www.youtube.com/watch?v=EPaSmQ2vtek
public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }
    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRender : MonoBehaviour
{
    private MapGen map;
    [Header("Settings for tiles")]
    public float innerSize;
    public float outerSize;
    public float height;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public Material material;
    private List<Face> faces;

    [Header("Settings for Wave function collapse")]
    public int x;
    public int y;
    public List<GameObject> acceptableTiles = new List<GameObject>();
    [Serializable]
    public struct Entropy
    {
        public double UpperAcceptable;
        public double LowerAcceptable;
        public bool Collapsed;
    }
    [Serializable]
    public struct Neigbours
    {
        public GameObject Up;
        public GameObject Down;
        public GameObject TopLeft;
        public GameObject TopRight;
        public GameObject BottomLeft;
        public GameObject BottomRight;
    }
    public Neigbours neigbhours;
    public Entropy entropy;
    public List<GameObject> GameTiles = new List<GameObject>();

    #region MAKE_TILE
    private void Awake()
    {
        map = MapGen.instance;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        mesh = new Mesh();
        mesh.name = "Hex";

        meshFilter.mesh = mesh;
        //meshRenderer.material = material;
    }

    private void Start()
    {
        meshRenderer.material = material;
    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    void DrawFaces()
    {
        faces = new List<Face>();

        //Top faces
        for(int point = 0; point < 6; point++)
        {
            faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        }
    }

    void CombineFaces()
    { 
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for(int i = 0; i < faces.Count; i++) 
        {
            //Add Verts
            vertices.AddRange(faces[i].vertices);
            uvs.AddRange(faces[i].uvs);

            //Offset Triangles
            int offset = (4 * i);
            foreach(int triangle in faces[i].triangles)
            { tris.Add(triangle + offset); }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
    }

    Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int> { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
        if(reverse)
        { vertices.Reverse(); }
        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = 60 * index;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3(size * Mathf.Cos(angle_rad), height, size * Mathf.Sin(angle_rad));
    }
    #endregion

    #region WAVE_FUNCTION_COLLAPSE
    public void GetNeigbhours()
    {
        int up = 1;
        int down = -1;
        string Upname;
        string Downname;
        string TopLeft;
        string TopRight;
        string BottomLeft;
        string BottomRight;
        if(x % 2 == 0)
        { 
            TopLeft = "Hex " + (x + up).ToString() + "," + y.ToString();
            TopRight = "Hex " + (x + down).ToString() + "," + y.ToString();
            BottomLeft = "Hex " + (x + up).ToString() + "," + (y+down).ToString();
            BottomRight = "Hex " + (x + down).ToString() + "," + (y+down).ToString();
        }
        else
        {
            TopLeft = "Hex " + (x + up).ToString() + "," + (y+up).ToString();
            TopRight = "Hex " + (x + down).ToString() + "," + (y+up).ToString();
            BottomLeft = "Hex " + (x + up).ToString() + "," + y.ToString();
            BottomRight = "Hex " + (x + down).ToString() + "," + y.ToString();
        }
        Upname = "Hex " + x.ToString() + "," + (y + up).ToString();
        Downname = "Hex " + x.ToString() + "," + (y + down).ToString();
        
        neigbhours.Up = GameObject.Find(Upname);
        neigbhours.Down = GameObject.Find(Downname);
        neigbhours.TopLeft = GameObject.Find(TopLeft);
        neigbhours.TopRight = GameObject.Find(TopRight);
        neigbhours.BottomLeft = GameObject.Find(BottomLeft);
        neigbhours.BottomRight = GameObject.Find(BottomRight);
        

    }

    public void ReduceEntropy(GameObject newTile)
    {
        //acceptableTiles.Clear();
        //Debug.Log("Name of the new tile is " + newTile.name);
        float UpperAve = 0f;
        float LowerAve = 0f;
        GameTiles.Add(newTile);
     
        if(GameTiles.Count ==  1)
        {
            var data = newTile.GetComponent<Tile>();
            entropy.UpperAcceptable = Mathf.Abs(newTile.transform.localScale.z) + data.AcceptableRange;
            entropy.LowerAcceptable = Mathf.Abs(newTile.transform.localScale.z) - data.AcceptableRange;
            entropy.UpperAcceptable = Math.Round(entropy.UpperAcceptable, 2);
            entropy.LowerAcceptable = Math.Round(entropy.LowerAcceptable, 2);
        }
        else
        {
            foreach(GameObject tile in GameTiles)
            {
                var data = tile.GetComponent<Tile>();
                UpperAve += Mathf.Abs(tile.transform.localScale.z) + data.AcceptableRange;
                LowerAve += Mathf.Abs(tile.transform.localScale.z) - data.AcceptableRange;
            }
            //entropy.UpperAcceptable = (Mathf.Round(UpperAve * 100)) / 100.0;
            //entropy.LowerAcceptable = (Mathf.Round(LowerAve * 100)) / 100.0;
            entropy.UpperAcceptable = Math.Round(UpperAve, 2) / GameTiles.Count;
            entropy.LowerAcceptable = Math.Round(LowerAve, 2) / GameTiles.Count;

        }

        if(entropy.UpperAcceptable == 1.49 && entropy.LowerAcceptable == 1.49)
        { entropy.UpperAcceptable = 2.00; }

        if(entropy.UpperAcceptable > 20)
        { entropy.UpperAcceptable = 20; }

        if(entropy.LowerAcceptable < 0)
        { entropy.LowerAcceptable = 0; }

        if(entropy.LowerAcceptable >= 20)
        { entropy.LowerAcceptable = 12; }
        List<GameObject> PList = new List<GameObject>();
        foreach (GameObject tile in map.Tiles)
        {
            var data = tile.GetComponent<Tile>();
            //Debug.Log("Now checking against tile " + tile.name);
            if (entropy.LowerAcceptable > data.LowerRange && entropy.UpperAcceptable < data.UpperRange)
            { PList.Add(tile); }
        }

        if(PList.Count == 0)
        { 
            PList.Add(map.Tiles[3]);
            entropy.UpperAcceptable = 6f;
            entropy.LowerAcceptable = 1f;
        }
        acceptableTiles = PList;
    }

    #endregion
}
