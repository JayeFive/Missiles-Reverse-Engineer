﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour {

    private Airplane airplane;
    private Rigidbody2D rb2D;

    [HideInInspector] public float flightSpeed;
    [HideInInspector] public float turnSpeed;
    [HideInInspector] public float lifeSpan;

    private Vector2 vectorToAirplane;
    private float flightDirection;
    private Quaternion qt;

    public bool missleParamsLoaded = false;
    private bool isActive = true;

    // Use this for initialization
    void Start ()
    {
        airplane = FindObjectOfType<Airplane>();
        rb2D = GetComponent<Rigidbody2D>();

        StartCoroutine(StartMissle());
    }
	
	// Update is called once per frame
	void Update ()
    {
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, flightSpeed);

        if (isActive)
        {
            MoveMissle();
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(MissleFade());
        }
    }

    private void MoveMissle()
    {
        DetermineDirection();
        TurnAndPush();
    }

    private void DetermineDirection()
    {
        vectorToAirplane = airplane.transform.position - transform.position;
        flightDirection = Mathf.Atan2(vectorToAirplane.y, vectorToAirplane.x) * Mathf.Rad2Deg;
        qt = Quaternion.AngleAxis(flightDirection, Vector3.forward);
    }

    private void TurnAndPush()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * turnSpeed);
        rb2D.AddForce(transform.right * flightSpeed);
    }

    private IEnumerator StartMissle ()
    {
        yield return new WaitUntil(() => missleParamsLoaded == true);

        StartCoroutine(RunMissleLifeSpan());
    }

    private IEnumerator RunMissleLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);

        isActive = false;
    }

    private IEnumerator MissleFade()
    {
        for (float scale = 1; scale > 0; scale -= 0.015f)
        {
            transform.localScale = new Vector3(scale, scale, 0);
            yield return null;
        }

        Destroy(gameObject);
    }
}
