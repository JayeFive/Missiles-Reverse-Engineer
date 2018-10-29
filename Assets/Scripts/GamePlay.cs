using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePlay : MonoBehaviour {

    [SerializeField] private Canvas worldSpaceCanvas;

    private Airplane airplane;

    private int stars = 0;
    private int bonus = 0;

public int Stars
    {
        get { return stars; }
        set
        {
            stars = value;
            GameManager.Instance.UpdateStarScore(stars);
        }
    }

    public int Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }

    // MonoBehavior
    void Start ()
    {
        airplane = FindObjectOfType<Airplane>();

        // TODO create coroutine utility class to return bool value from this coroutine
        // https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html
        airplane.StartCoroutine("StartingTurn");
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
