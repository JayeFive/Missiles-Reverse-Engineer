using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumMissleTriple : MonoBehaviour {

    const int numMissles = 3;

    private Airplane airplane;
    public GameObject mediumMissle;

    [SerializeField] Vector2 spawnOffset;
    [SerializeField] float horizontalRangeFloor;
    [SerializeField] float horizontalRangeCeiling;
    [SerializeField] float timeToLaunch;
    [SerializeField] float timeBetweenFloor;
    [SerializeField] float timeBetweenCeiling;

    private float timeBetweenMissles = 0.0f;

    void Start ()
    {
        airplane = FindObjectOfType<Airplane>();
    }

    public IEnumerator StartLaunchController ()
    {
        yield return new WaitForSeconds(timeToLaunch);

        for (int i = 0; i < numMissles; i++)
        {
            StartCoroutine(LaunchMissle(timeBetweenMissles));
            timeBetweenMissles += Random.Range(timeBetweenFloor, timeBetweenCeiling);
        }
    }

    private IEnumerator LaunchMissle (float t)
    {
        yield return new WaitForSeconds(t);

        var horizontalOffset = RandomizeHorizontal(spawnOffset);

        Vector3 spawnLocation = airplane.transform.position + horizontalOffset;
        Vector2 vectorToAirplane = airplane.transform.position - spawnLocation;
        float angle = Mathf.Atan2(vectorToAirplane.y, vectorToAirplane.x) * Mathf.Rad2Deg;
        Quaternion startingRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject newMissle = Instantiate(mediumMissle, spawnLocation, startingRotation);
        Debug.Log("Missle spawned");
    }

    private Vector3 RandomizeHorizontal (Vector3 offset)
    {
        var xPos = Random.Range(horizontalRangeFloor, horizontalRangeCeiling);
        offset.x += xPos;

        return offset;
    }
}

