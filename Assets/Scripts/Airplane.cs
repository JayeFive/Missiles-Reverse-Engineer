using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {

    public TouchJoystick joystick;
    public float flightSpeed = 1.0f;
    public float turnSpeed = 90.0f;
    public bool startingTurnComplete = false;

    private float flightDirection;

	// Use this for initialization
	void Start ()
    {
        joystick = FindObjectOfType<TouchJoystick>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.right * flightSpeed * Time.deltaTime;

        if (startingTurnComplete)
        {
            if (Input.GetMouseButton(0))
            {
                flightDirection = Mathf.Atan2(joystick.joystickDirection.y, joystick.joystickDirection.x) * Mathf.Rad2Deg;
                TurnAirplane(flightDirection);
            }
        }
    }

    public IEnumerator StartingTurn ()
    {
        for (float flightDirection = 90; flightDirection <= 270; flightDirection += Time.deltaTime * turnSpeed)
        {
            TurnAirplane(flightDirection);
            yield return null;
        }

        startingTurnComplete = true;
    }

    void TurnAirplane (float flightDirection)
    {
        Quaternion qt = Quaternion.AngleAxis(flightDirection, Vector3.forward);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * turnSpeed);

    }
}
