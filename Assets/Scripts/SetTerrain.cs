using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://answers.unity.com/questions/391688/how-to-set-terrain-setheights-c.html
public class SetTerrain : MonoBehaviour {
    public Terrain terrain;
    public TerrainData tData;

    public int xRes;
    public int yRes;
    public float[, ] heights;
    //public Terrain myTerrain;
    public int PlayerXi;
    public int PlayerZi;
    private GameObject Player;
    void Start () {
        Player = GameObject.Find ("Camera");
        tData = terrain.terrainData;
        xRes = tData.heightmapResolution;
        yRes = tData.heightmapResolution;
    }

    void Update () {
        // we convert float of our position x in to int
        PlayerXi = (int) Player.transform.position.x;
        // we convert float of our position z in to int
        PlayerZi = (int) Player.transform.position.z;
        // we tell heights how big the map is
        heights = tData.GetHeights (0, 0, xRes, yRes);
        // we set z in to x AND x in to y
        heights[PlayerZi, PlayerXi] = 0.01f;
        tData.SetHeights (0, 0, heights);
    }

}