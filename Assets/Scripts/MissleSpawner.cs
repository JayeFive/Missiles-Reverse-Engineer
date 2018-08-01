using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : MonoBehaviour {

    public Airplane airplane;
    public GameObject longMissle;

    private float currentTime;

    void Start ()
    {
        airplane = FindObjectOfType<Airplane>();
        Invoke("SpawnMissle", 2);
    }

    void Update ()
    {
        //currentTime = FindObjectOfType<Timer>().currentTime;
    }

    public void SpawnMissle()
    {
        Vector3 spawnLocation = airplane.transform.position + (Vector3.up * 12);
        Vector2 vectorToAirplane = airplane.transform.position - spawnLocation;
        float angle = Mathf.Atan2(vectorToAirplane.y, vectorToAirplane.x) * Mathf.Rad2Deg;
        Quaternion startingRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject newMissle = Instantiate(longMissle, spawnLocation, startingRotation);
    }
}
