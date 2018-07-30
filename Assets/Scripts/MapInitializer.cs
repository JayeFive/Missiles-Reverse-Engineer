using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializer : MonoBehaviour {

    public GameObject mapTile;
    public GameObject cloud;
    public GameObject map;
    public int squareMapSideSize = 3;

    private int tileSize;

    const int numCloudsFloor = 3;
    const int numCloudsCeiling = 7;

	// Use this for initialization
	void Start ()
    {
        tileSize = (int)mapTile.GetComponent<RectTransform>().rect.width;

        int halfNumColumns = (int)Mathf.Floor(squareMapSideSize / 2);
        int firstX = halfNumColumns * -tileSize;
        int firstY = halfNumColumns * tileSize; ;

        if ((squareMapSideSize % 2) == 0)
        {
            firstX += tileSize / 2;
            firstY -= tileSize / 2;
        }

        int tileX;
        int tileY;

        for (tileY = firstY; tileY >= firstX; tileY -= tileSize)
        {
            for (tileX = firstX; tileX <= firstY; tileX += tileSize)
            {
                GameObject newTile = Instantiate(mapTile, new Vector3(tileX, tileY, 2.0f), Quaternion.identity, transform);
                AddClouds(newTile);
            }
        }

        map.GetComponent<MapController>().PopulateMapTileArray();
    }

    void AddClouds (GameObject newTile)
    {
        int numClouds = (int)Mathf.Floor(Random.Range(numCloudsFloor, numCloudsCeiling));
        
        for (int i = 0; i < numClouds; i++)
        {
            GameObject newCloud = Instantiate(cloud, newTile.transform);
            newCloud.transform.localPosition = new Vector3(Random.Range(-tileSize / 2, tileSize / 2), Random.Range(-tileSize / 2, tileSize / 2), 0); 
            newCloud.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        }
        
    }
}
