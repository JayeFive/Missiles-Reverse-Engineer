using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {

    public TouchJoystick joystick;
    public float flightSpeed = 1.0f;
    public float turnSpeed = 10.0f;
    public bool gameStarted = false;

    private float flightDirection;
    private Vector3 rotation;

	// Use this for initialization
	void Start ()
    {
        joystick = FindObjectOfType<TouchJoystick>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up * flightSpeed * Time.deltaTime;

        if (gameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                flightDirection = Mathf.Atan2(-joystick.joystickDirection.x, joystick.joystickDirection.y) * Mathf.Rad2Deg;
                Debug.Log(flightDirection);
                TurnAirplane(flightDirection);
            }
        }
    }

    public IEnumerator StartingTurn ()
    {
        for (float flightDirection = 0; flightDirection <= 180; flightDirection += Time.deltaTime * turnSpeed)
        {
            TurnAirplane(flightDirection);
            yield return null;
        }

        gameStarted = true;
    }

    void TurnAirplane (float flightDirection)
    {
        transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler(0, 0, flightDirection), Time.deltaTime * turnSpeed);
    }
}
