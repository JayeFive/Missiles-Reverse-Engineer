using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongMissle : MonoBehaviour {

    private MissleController missleController;

    [SerializeField] float flightSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float lifeSpan;

    // Use this for initialization
    void Start()
    {
        missleController = GetComponent<MissleController>();

        missleController.flightSpeed = flightSpeed;
        missleController.turnSpeed = turnSpeed;
        missleController.lifeSpan = lifeSpan;
    }
}
