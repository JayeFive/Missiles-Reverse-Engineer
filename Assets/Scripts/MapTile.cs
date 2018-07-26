using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

    MapController mapController;

    void Start ()
    {
        mapController = FindObjectOfType<MapController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mapController.ShiftMap(gameObject);
    }
}
