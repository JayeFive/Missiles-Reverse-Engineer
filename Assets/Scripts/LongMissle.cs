using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongMissle : MonoBehaviour {

    [SerializeField] float flightSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float lifeSpan;

    private MissleController missleController;

    // Use this for initialization
    void Start()
    {
        missleController = GetComponent<MissleController>();
        SetMissleProperties();
    }

    private void SetMissleProperties()
    {
        missleController.flightSpeed = flightSpeed;
        missleController.turnSpeed = turnSpeed;
        missleController.lifeSpan = lifeSpan;
    }
}
