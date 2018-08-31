using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class OffscreenIndicator : MonoBehaviour {

    public GameObject indicatorSprite;
    private Airplane airplane;
    private GameObject indicator;

    // Camera fields
    private Vector2 screenLimit;
    private Vector2 airplaneOffset;
    private bool isAboveAirplane;
    private float cameraPos;
    private float airplaneAxisOffset;
    private float screenAxisLimit;
    private float distToObj;

    //Trig fields
    private Vector2 toObj;
    private Vector2 axis;
    private float adjacent;
    private float hypotenuse;
    private float theta;
    private float pos;
    private float airplanePos;


    // Monobehaviors
    void Start()
    {
        airplane  = FindObjectOfType<Airplane>();
        indicator = Instantiate(indicatorSprite, transform.position, Quaternion.identity);

        indicator.transform.parent = gameObject.transform;
        screenLimit = DetermineScreenLimits();
    }   

    void Update()
    {
        isAboveAirplane = IsAbove();
        toObj = transform.position - airplane.transform.position;

        SetAxisHorizontal();
        PerformTrig();
        
        indicator.transform.position = (Vector2)(airplane.transform.position) + (toObj.normalized * hypotenuse);
    }

    void FixedUpdate ()
    {
        airplaneOffset = DetermineAirplaneOffset();
    }


    // Camera methods
    private Vector2 DetermineScreenLimits ()
    {
        float x = (Camera.main.orthographicSize * Screen.width / Screen.height) - (indicatorSprite.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        float y = (Camera.main.orthographicSize) - (indicatorSprite.GetComponent<SpriteRenderer>().bounds.size.x / 2);

        return new Vector2(x, y);
    }

    private bool IsAbove()
    {
        return transform.position.y > airplane.transform.position.y ? true : false;
    }

    private Vector2 DetermineAirplaneOffset ()
    {
        float x = Camera.main.transform.position.x - airplane.transform.position.x;
        float y = Camera.main.transform.position.y - airplane.transform.position.y;

        return new Vector2(x, y);
    }

    private float FindAngleToCorner ()
    {
        return Vector2.Angle(CreateVectorToCorner(), Vector2.right);
    }

    private Vector2 CreateVectorToCorner ()
    {
        return FindWorldCorner(isAboveAirplane ? 1 : -1) - (Vector2)airplane.transform.position;
    }

    private Vector2 FindWorldCorner (int modY)
    {
        float x = Camera.main.transform.position.x + screenLimit.x;
        float y = Camera.main.transform.position.y + (screenLimit.y * modY);

        return new Vector2(x, y);
    }


    // Trig methods
    private void SetAxisHorizontal ()
    {
        axis = Vector2.right;
        pos = transform.position.x;
        airplanePos = airplane.transform.position.x;
        cameraPos = Camera.main.transform.position.x;
        airplaneAxisOffset = airplaneOffset.x;
        screenAxisLimit = screenLimit.x;
    }

    private void SetAxisVertical ()
    {
        axis = Vector2.up;
        pos = transform.position.y;
        airplanePos = airplane.transform.position.y;
        cameraPos = Camera.main.transform.position.y;
        airplaneAxisOffset = airplaneOffset.y;
        screenAxisLimit = screenLimit.y;
    }

    private void PerformTrig ()
    {
        FindTheta();
        SetAxis();
        FindAdjacent();
        FindDistToObj();
        ClampAdjacent();
        FindHypotenuse();
    }

    private void FindTheta()
    {
        theta = Vector2.Angle(toObj, axis);

        if (theta > 90)
        {
            theta = 180 - theta;
        }
    }

    private void SetAxis()
    {
        if (theta < FindAngleToCorner())
        {
            SetAxisHorizontal();
        }
        else
        {
            SetAxisVertical();
            FindTheta();
        }
    }

    private void FindAdjacent()
    {
        adjacent = Mathf.Abs(pos - airplanePos);
    }

    private void FindDistToObj ()
    {
        distToObj = pos - cameraPos;
    }

    private void ClampAdjacent()
    {
        if (Mathf.Abs(distToObj) > screenAxisLimit)
        {
            float normalized = distToObj / Mathf.Abs(distToObj);
            adjacent = screenAxisLimit + (airplaneAxisOffset * normalized);

            EnableIndicatorSprite(true);
        }
        else
        {
            EnableIndicatorSprite(false);
        }
    }

    private void FindHypotenuse()
    {
        theta *= Mathf.Deg2Rad;
        hypotenuse = adjacent / Mathf.Cos(theta);
    }


    // Hide and destroy methods
    private void EnableIndicatorSprite(bool show)
    {
        indicator.GetComponent<SpriteRenderer>().enabled = show;
    }

    public void DestroyIndicator ()
    {
        Destroy(indicator);
    }
}

