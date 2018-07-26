using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public GameObject airplane;
    public GameObject map;
    public GameObject mapTile;
    public float tileSize;

    private int squareMapSideSize;
    private float tileShiftModifier; 
    private MapTile[] mapTiles;

    void Start ()
    {
        squareMapSideSize = GetComponent<MapInitializer>().squareMapSideSize;
        tileShiftModifier = squareMapSideSize / 2.0f;
    }

    public void PopulateMapTileArray ()
    {
        mapTiles = GetComponentsInChildren<MapTile>();
    }

    public void ShiftMap (GameObject centerTile)
    {
        foreach (MapTile tile in mapTiles)
        {
            float xOffset = centerTile.transform.position.x - tile.transform.position.x;
            float yOffset = centerTile.transform.position.y - tile.transform.position.y;

            if (Mathf.Abs(xOffset) == tileSize * (squareMapSideSize - 1))
            {
                Vector3 newPos = new Vector3(xOffset * tileShiftModifier, 0, 0);
                ShiftTile(tile, newPos);
            }

            if (Mathf.Abs(yOffset) == tileSize * (squareMapSideSize - 1))
            {
                Vector3 newPos = new Vector3(0, yOffset * tileShiftModifier, 0);
                ShiftTile(tile, newPos);
            }
        }
    }

    private void ShiftTile(MapTile tile, Vector3 newPos)
    {
        tile.transform.Translate(newPos, Space.Self);
    }
}
