using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongMissle : MonoBehaviour {

    [SerializeField] float flightSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float lifeSpan;

    private MissleController missleController;

    void Start()
    {
        missleController = GetComponent<MissleController>();
        SetMissleParams();
        missleController.missleParamsLoaded = true;
    }

    private void SetMissleParams()
    {
        missleController.flightSpeed = flightSpeed;
        missleController.turnSpeed = turnSpeed;
        missleController.lifeSpan = lifeSpan;
    }
}
