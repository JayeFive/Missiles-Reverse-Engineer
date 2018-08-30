using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class OffscreenIndicator : MonoBehaviour {

    private Airplane airplane;
    [SerializeField] GameObject indicatorSprite;
    GameObject indicator;

    private Vector2 screenLimit;
    private Vector2 airplaneOffset;
    private bool isAboveAirplane;

    [HideInInspector] public float cameraPos;
    [HideInInspector] public float airplaneAxisOffset;
    [HideInInspector] public float screenAxisLimit;
    [HideInInspector] public float distToObj;

    private Vector2 toObj;
    private Vector2 axis;
    private float adjacent;
    private float hypotenuse;
    private float theta;

    private float pos;
    private float airplanePos;

    // Use this for initialization
    void Start()
    {
        airplane    = FindObjectOfType<Airplane>();
        indicator   = Instantiate(indicatorSprite, Camera.main.transform.position, Quaternion.identity);
        screenLimit = DetermineScreenLimits();
    }   

    // Update is called once per frame
    void Update()
    {
        isAboveAirplane = IsAbove();
        toObj = transform.position - airplane.transform.position;

        SetAxisHorizontal();

        FindTheta();
        SetAxis();
        FindAdjacent();
        FindDistToObj();
        ClampAdjacent();
        FindHypotenuse();

        indicator.transform.position = (Vector2)(airplane.transform.position) + (toObj.normalized * hypotenuse);
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

    public float FindAngleToCorner ()
    {
        return Vector2.Angle(CreateVectorToCorner(), transform.right);
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

    public void SetAxisHorizontal ()
    {
        axis = transform.right;
        pos = transform.position.x;
        airplanePos = airplane.transform.position.x;
        cameraPos = Camera.main.transform.position.x;
        airplaneAxisOffset = airplaneOffset.x;
        screenAxisLimit = screenLimit.x;

    }

    public void SetAxisVertical ()
    {
        axis = transform.up;
        pos = transform.position.y;
        airplanePos = airplane.transform.position.y;
        cameraPos = Camera.main.transform.position.y;
        airplaneAxisOffset = airplaneOffset.y;
        screenAxisLimit = screenLimit.y;
    }

    private void FindDistToObj ()
    {
        distToObj = pos - cameraPos;
    }

    public void FindTheta()
    {
        theta = Vector2.Angle(toObj, axis);

        if (theta > 90)
        {
            theta = 180 - theta;
        }

    }

    public void SetAxis()
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

    public void FindAdjacent()
    {
        adjacent = Mathf.Abs(pos - airplanePos);
    }

    public void ClampAdjacent()
    {
        if (Mathf.Abs(distToObj) > screenAxisLimit)
        {
            float normalized = distToObj / Mathf.Abs(distToObj);

            adjacent = screenAxisLimit + (airplaneAxisOffset * normalized);
        }
    }

    public void FindHypotenuse()
    {
        theta *= Mathf.Deg2Rad;
        hypotenuse = adjacent / Mathf.Cos(theta);
    }
}
