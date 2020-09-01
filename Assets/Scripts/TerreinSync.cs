using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerreinSync : MonoBehaviour {
    [SerializeField]
    Terrain terr; // terrain to modify
    int hmWidth; // heightmap width
    int hmHeight; // heightmap height

    int posXInTerrain; // position of the game object in terrain width (x axis)
    int posYInTerrain; // position of the game object in terrain height (z axis)
    [SerializeField]
    int size = 50; // the diameter of terrain portion that will raise under the game object
    float desiredHeight = 0; // the height we want that portion of terrain to be
    [SerializeField] float add = 0;
    int coeffX, coeffY;
    void Start () {

        // terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapResolution;
        hmHeight = terr.terrainData.heightmapResolution;

    }

    void Update () {
        desiredHeight = (transform.position.y + add) / 500;

        // get the normalized position of this game object relative to the terrain
        Vector3 tempCoords = (transform.position - terr.gameObject.transform.position);
        Vector3 coords;
        coords.x = tempCoords.x / terr.terrainData.size.x;
        coords.y = tempCoords.y / terr.terrainData.size.y;
        coords.z = tempCoords.z / terr.terrainData.size.z;

        // get the position of the terrain heightmap where this game object is
        posXInTerrain = (int) (coords.x * hmWidth);
        posYInTerrain = (int) (coords.z * hmHeight);

        // we set an offset so that all the raising terrain is under this game object
        int offset = size / 2;

        // get the heights of the terrain under this game object
        float[, ] heights = terr.terrainData.GetHeights (posXInTerrain - offset, posYInTerrain - offset, size, size);

        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                heights[i, j] = desiredHeight;
        // go raising the terrain slowly
        //desiredHeight += Time.deltaTime;
        // set the new height
        //gradus =0, y=20,x=0,
        //gradus = 90, y=0,x=20
        //gradus = -90,y=0,x=20
        coeffX = (int) (20 * (transform.rotation.y));
        Debug.Log ($"transom mod {20 * (transform.rotation.y)}");
        coeffY = (int) (20 * (1 - transform.rotation.y));
        //Debug.Log ($"coeffY {coeffY},coeffX {coeffX}");
        terr.terrainData.SetHeights ((posXInTerrain), (posYInTerrain), heights);

    }

}