using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

    GameObject map;

    void Start ()
    {
        map = GameObject.FindGameObjectWithTag("Map");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        map.GetComponent<MapController>().ShiftMap();
    }
}
