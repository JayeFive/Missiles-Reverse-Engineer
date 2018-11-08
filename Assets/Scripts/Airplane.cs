using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    private GamePlay gamePlay;

    [SerializeField] float flightSpeed = 0f;
    [SerializeField] float turnSpeed = 0f;
    [SerializeField] TouchJoystick joystick;
    private bool _startingTurnComplete = false;

    private float flightDirection;
    private Rigidbody2D rb2D;


    // MonoBehavior
    void Awake()
    {
        GameManager.Instance.Airplane = this;
    }


	void Start()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        rb2D = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<TouchJoystick>();
	}
	

	// MonoBehavior
	void Update()
    {
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, flightSpeed);

        rb2D.AddForce(transform.right * flightSpeed);

        if (_startingTurnComplete)
        {
            if (Input.GetMouseButton(0))
            {
                flightDirection = Mathf.Atan2(joystick.joystickDirection.y, joystick.joystickDirection.x) * Mathf.Rad2Deg;
                TurnAirplane(flightDirection);
            }
        }
    }


    // Airpane control
    public IEnumerator StartingTurn()
    {
        for (float flightDirection = 90; flightDirection <= 270; flightDirection += Time.deltaTime * turnSpeed)
        {
            TurnAirplane(flightDirection);
            yield return null;
        }

        if (joystick != null)
        {
            joystick.GetComponent<ETCJoystick>().visible = true;
        }
        else Debug.Log("Joystick not found!");

        _startingTurnComplete = true;
        GameManager.Instance.PlayerStart();
    }

    void TurnAirplane(float flightDirection)
    {
        Quaternion qt = Quaternion.AngleAxis(flightDirection, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * turnSpeed);
    }
}
