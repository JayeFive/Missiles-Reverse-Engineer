using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    private Airplane airplane;
    public TouchJoystick joystick;
    bool gameStarted = false;


	// Use this for initialization
	void Start ()
    {
        airplane = FindObjectOfType<Airplane>();
        joystick = FindObjectOfType<TouchJoystick>();

        // TODO create coroutine utility class to return bool value from this coroutine
        // https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html
        airplane.StartCoroutine("StartingTurn");
	}
	
	// Update is called once per frame
	void Update () {
		
        if (airplane.startingTurnComplete)
        {
            joystick.GetComponent<ETCJoystick>().visible = true;
        }
    }


}
