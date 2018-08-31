using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    private Airplane airplane;
    public TouchJoystick joystick;

    public int starScore = 0;

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
		
        // TODO move to airplane script
        if (airplane.startingTurnComplete)
        {
            joystick.GetComponent<ETCJoystick>().visible = true;
        }
    }

    public void ShowResetUI ()
    {
        // TODO Show Reset UI Screen
    }
}
