using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchJoystick : MonoBehaviour {

    public Vector3 joystickDirection = new Vector3();

	// Use this for initialization
	void Start () {
        joystickDirection.z = 0;
	}
	
	// Update is called once per frame
	void Update () {

        float xpos = ETCInput.GetAxis("Horizontal");
        float ypos = ETCInput.GetAxis("Vertical");
        joystickDirection.x = xpos;
        joystickDirection.y = ypos;
    }

    void RotateAirplane ()
    {

    }

}
