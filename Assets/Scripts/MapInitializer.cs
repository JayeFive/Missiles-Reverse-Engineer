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
        Debug.Log(halfNumColumns);

        if ((squareMapSideSize % 2) == 0)
        {
            firstX = (halfNumColumns * -tileSize) + tileSize / 2;
            firstY = (halfNumColumns * tileSize) - tileSize / 2;
        }
        else
        {
            firstX = halfNumColumns * -tileSize;
            firstY = halfNumColumns * tileSize;
        }

        int tileX;
        int tileY;

        for (tileY = firstY; tileY >= firstX; tileY -= tileSize)
        {
            for (tileX = firstX; tileX <= firstY; tileX += tileSize)
            {
                Instantiate(mapTile, new Vector3(tileX, tileY, 0), Quaternion.identity, transform);
                //AddClouds();
            }
        }
    }

    void AddClouds ()
    {
        int numClouds = (int)Mathf.Floor(Random.Range(1.0f, 3.0f));

        
    }
}
