using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializer : MonoBehaviour {

    public GameObject mapTile;
    public GameObject cloud;
    public int squareMapSideSize = 6;

	// Use this for initialization

	void Start () {

        int tileSize = (int)mapTile.GetComponent<RectTransform>().rect.width;

        int halfNumColumns = (int)Mathf.Floor(squareMapSideSize / 2);
        int firstX;
        int firstY;

        firstX = halfNumColumns * -tileSize;
        firstY = halfNumColumns * tileSize;

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
                GameObject newTile = Instantiate(mapTile, new Vector3(tileX, tileY, 0), Quaternion.identity, transform);
                newTile.transform.Translate(0, 0, 2.0f);
                AddClouds(newTile);
            }
        }
    }

    void AddClouds (GameObject newTile)
    {
        int numClouds = (int)Mathf.Floor(Random.Range(1.0f, 3.0f));
        
        for (int i = 0; i < numClouds; i++)
        {
            GameObject newCloud = Instantiate(cloud, newTile.transform);
            newCloud.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0);
            newCloud.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        }
        
    }
}
