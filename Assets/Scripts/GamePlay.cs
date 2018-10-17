using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    private Airplane airplane;
    public TouchJoystick joystick;
    private Text starScoreText;

    private int starScore = 0;

    public int StarScore
    {
        get { return starScore; }
        set
        {
            starScore = value;
            starScoreText.GetComponent<Text>().text = starScore.ToString();
        }
    }

	// MonoBehavior
	void Start ()
    {
        airplane = FindObjectOfType<Airplane>();
        joystick = FindObjectOfType<TouchJoystick>();
        starScoreText = GameObject.Find("StarCounterText").GetComponent<Text>();

        // TODO create coroutine utility class to return bool value from this coroutine
        // https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html
        airplane.StartCoroutine("StartingTurn");
    }
	
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

    // Screen Parameters
    public Vector2 DetermineScreenLimits()
    {
        float x = (Camera.main.orthographicSize * Screen.width / Screen.height);
        float y = (Camera.main.orthographicSize);

        return new Vector2(x, y);
    }
}
