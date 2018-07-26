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

        //airplane.StartingTurn();
        airplane.StartCoroutine("StartingTurn");
	}
	
	// Update is called once per frame
	void Update () {
		
        if (airplane.gameStarted)
        {
            joystick.GetComponent<ETCJoystick>().visible = true;
        }

    }
}
