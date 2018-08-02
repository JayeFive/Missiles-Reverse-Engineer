using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : MonoBehaviour {

    [SerializeField] float spawnDistance;
    [SerializeField] Vector3 topSpawnLocation;

    public Airplane airplane;
    public GameObject longMissle;

    MediumMissleTriple mediumMissleTriple;

    void Start ()
    {
        mediumMissleTriple = GetComponent<MediumMissleTriple>();

        StartCoroutine(mediumMissleTriple.StartLaunchController(2)); // ~20 second flight time

    }
}
