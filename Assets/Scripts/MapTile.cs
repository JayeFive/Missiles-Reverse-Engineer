using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

    MapController mapController;

    void Awake ()
    {
        mapController = FindObjectOfType<MapController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Airplane")
        {
            mapController.ShiftMap(gameObject);
        }
    }
}
