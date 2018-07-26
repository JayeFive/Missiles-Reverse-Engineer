using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public GameObject airplane;
    public GameObject map;
    public GameObject mapTile;

    private int squareMapSideSize;
    private MapTile[] mapTiles;


    void Start ()
    {
        squareMapSideSize = GetComponent<MapInitializer>().squareMapSideSize;
    }

	void Update ()
    {

	}

    public void PopulateMapTileArray ()
    {
        mapTiles = GetComponentsInChildren<MapTile>();
    }

    public void ShiftMap ()
    {
        float xOffset;
        float xOffsetAbs;
        float yOffset;
        float yOffsetAbs;
        bool isMovingLeft;
        bool isMovingDown;

        foreach (MapTile tile in mapTiles)
        {

            xOffset = airplane.transform.position.x - tile.transform.position.x;
            xOffsetAbs = Mathf.Abs(xOffset);

            yOffset = airplane.transform.position.y - tile.transform.position.y;
            yOffsetAbs = Mathf.Abs(yOffset);


            if (xOffsetAbs >= tile.GetComponent<BoxCollider2D>().bounds.size.x * 3.0f)
            {
                isMovingLeft = GetShiftDirection(xOffset);
                ShiftBoxHorizontally(tile, isMovingLeft);
                //Debug.Log("x offset:" + xOffsetAbs);
            }

            if (yOffsetAbs >= tile.GetComponent<BoxCollider2D>().bounds.size.y * 3.0f)
            {
                isMovingDown = GetShiftDirection(yOffset);
                ShiftBoxVertically(tile, isMovingDown);
                //Debug.Log("y offset:" + yOffsetAbs);
            }
        }

        //Debug.Log("ShiftMap() completed");
    }

    bool GetShiftDirection (float offset)
    {
        if (offset < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ShiftBoxHorizontally (MapTile tile, bool isMovingLeft)
    {
        float directionModifier = 1;
        Vector3 newPos;

        if (isMovingLeft)
        {
            directionModifier *= -1;
        }

        newPos = new Vector3(tile.GetComponent<BoxCollider2D>().bounds.size.x * map.GetComponent<MapInitializer>().squareMapSideSize * directionModifier, 0, 0);

        tile.transform.Translate(newPos, Space.Self);
    }

    void ShiftBoxVertically (MapTile tile, bool isMovingDown)
    {
        float directionModifier = 1;
        Vector3 newPos;

        if (isMovingDown)
        {
            directionModifier *= -1;
        }

        newPos = new Vector3(0, tile.GetComponent<BoxCollider2D>().bounds.size.y * map.GetComponent<MapInitializer>().squareMapSideSize * directionModifier, 0);

        tile.transform.Translate(newPos, Space.Self);
    }
}
