using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongMissle : MonoBehaviour {

    Airplane airplane;

    [SerializeField] float flightSpeed = 2.0f;
    [SerializeField] float turnSpeed = 60.0f;

    private Rigidbody2D rb2D;
    private Vector2 vectorToAirplane;
    private float flightDirection;
    private Quaternion qt;
    private bool isActive = true;

    void Awake ()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        airplane = FindObjectOfType<Airplane>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, flightSpeed);

        if (isActive)
        {
            vectorToAirplane = airplane.transform.position - transform.position;
            flightDirection = Mathf.Atan2(vectorToAirplane.y, vectorToAirplane.x) * Mathf.Rad2Deg;
            qt = Quaternion.AngleAxis(flightDirection, Vector3.forward);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * turnSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * turnSpeed);

            rb2D.AddForce(transform.right * flightSpeed);
        }
        else
        {
            // TODO slow down and animate smaller coroutine until missle disappears
        }
    }

    void FindStartingDirection ()
    {

    }
}
