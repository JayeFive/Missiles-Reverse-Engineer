using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreeenIndicator : MonoBehaviour {

    private Airplane airplane;
    [SerializeField] GameObject indicatorSprite;
    GameObject indicator;

    private Vector2 screenLimit;
    private Vector2 airplaneOffset;
    private bool isAboveAirplane;

    // Use this for initialization
    void Start()
    {
        airplane = FindObjectOfType<Airplane>();

        indicator = Instantiate(indicatorSprite, Camera.main.transform.position, Quaternion.identity);

        Vector2 cameraPos = Camera.main.transform.position;
        float airplaneOffsetY = airplane.transform.position.y - cameraPos.y;

        screenLimit = DetermineScreenLimits();
    }

    // Update is called once per frame
    void Update()
    {
        isAboveAirplane = IsAbove();

        Vector2 toObj = transform.position - airplane.transform.position;
        float theta = Vector2.Angle(toObj, transform.right);
        float a = Mathf.Abs(transform.position.x - airplane.transform.position.x);

        if (theta > 90)
        {
            theta = 180 - theta;
        }

        if (theta < FindAngleToCorner())
        {
            if (a > screenLimit.x - airplaneOffset.x)
            {
                a = screenLimit.x - airplaneOffset.x;
            }
        }
        else
        {
            theta = Vector2.Angle(toObj, transform.up);

            if (theta > 90)
            {
                theta = 180 - theta;
            }

            a = Mathf.Abs(transform.position.y - airplane.transform.position.y);

            float bonusToCameraDistance = transform.position.y - Camera.main.transform.position.y;

            if (Mathf.Abs(bonusToCameraDistance) > screenLimit.y)
            {
                float normalized = bonusToCameraDistance / Mathf.Abs(bonusToCameraDistance);

                a = screenLimit.y + (airplaneOffset.y * normalized);
            }
        }

        theta *= Mathf.Deg2Rad;

        float h = a / Mathf.Cos(theta);

        indicator.transform.position = (Vector2)(airplane.transform.position) + (toObj.normalized * h);
    }

    void FixedUpdate ()
    {
        airplaneOffset = DetermineAirplaneOffset();

    }

    private Vector2 DetermineScreenLimits ()
    {
        float x = (Camera.main.orthographicSize * Screen.width / Screen.height) - (indicatorSprite.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        float y = (Camera.main.orthographicSize) - (indicatorSprite.GetComponent<SpriteRenderer>().bounds.size.x / 2);

        return new Vector2(x, y);
    }

    private Vector2 DetermineAirplaneOffset ()
    {
        float x = Camera.main.transform.position.x - airplane.transform.position.x;
        float y = Camera.main.transform.position.y - airplane.transform.position.y;

        return new Vector2(x, y);
    }

    private bool IsAbove()
    {
        return transform.position.y > airplane.transform.position.y ? true : false;
    }

    private float FindAngleToCorner ()
    {
        return Vector2.Angle(CreateVectorToCorner(), transform.right);
    }

    private Vector2 CreateVectorToCorner ()
    {
        return FindWorldCorner(isAboveAirplane ? 1 : -1) - (Vector2)airplane.transform.position;
    }

    private Vector2 FindWorldCorner (int negYMod)
    {
        float x = Camera.main.transform.position.x + screenLimit.x;
        float y = Camera.main.transform.position.y + (screenLimit.y * negYMod);

        return new Vector2(x, y);
    }
}
