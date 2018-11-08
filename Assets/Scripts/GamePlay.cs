using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePlay : MonoBehaviour {

    private int stars = 0;
    private int bonus = 0;
    private bool hasShield = false;
    private bool hasSpeedBonus = false;

public int Stars
    {
        get => stars;
        set
        {
            stars = value;
            GameManager.Instance.UpdateStarScore(stars);
        }
    }

    public int Bonus { get => bonus; set => bonus = value; }
    public bool HasShield { get => hasShield; set => hasShield = value; }
    public bool HasSpeedBonus { get => hasSpeedBonus; set => hasSpeedBonus = value; }


    // MonoBehavior
    void Start()
    {
        var camera = FindObjectOfType<Camera>();
        camera.transform.position = Camera.main.transform.position;
    }


    // Screen Parameters
    public Vector2 DetermineScreenLimits()
    {
        float x = (Camera.main.orthographicSize * Screen.width / Screen.height);
        float y = (Camera.main.orthographicSize);

        return new Vector2(x, y);
    }
}
