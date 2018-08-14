using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour {

    private GamePlay gamePlay;
    private Airplane airplane;
    private Rigidbody2D rb2D;
    private GameObject explosionController;
    private GameObject smokeTrail;

    [HideInInspector] public float flightSpeed;
    [HideInInspector] public float turnSpeed;
    [HideInInspector] public float lifeSpan;
    [HideInInspector] public bool missleParamsLoaded = false;

    private Vector2 vectorToAirplane;
    private float flightDirection;
    private Quaternion qt;

    private bool isActive = true;

    [SerializeField] float fadeSpeed = 0.015f;
    [SerializeField] float fadeSpeedModifier;
    private float fadeFlightSpeed;

    void Start ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        airplane = FindObjectOfType<Airplane>();
        smokeTrail = (GameObject)Resources.Load("Prefabs/smokeTrail");
        rb2D = GetComponent<Rigidbody2D>();
        explosionController = Resources.Load<GameObject>("Prefabs/ExplosionController");

        Instantiate(smokeTrail, transform);
        StartCoroutine(StartMissle());

        fadeFlightSpeed = flightSpeed * fadeSpeedModifier;
    }
	
	void Update ()
    {
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, flightSpeed);

        if (isActive)
        {
            MoveMissle();
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(MissleFade());
        }
    }

    private void MoveMissle()
    {
        DetermineDirection();
        TurnAndForce();
    }

    private void DetermineDirection()
    {
        vectorToAirplane = airplane.transform.position - transform.position;
        flightDirection = Mathf.Atan2(vectorToAirplane.y, vectorToAirplane.x) * Mathf.Rad2Deg;
        qt = Quaternion.AngleAxis(flightDirection, Vector3.forward);
    }

    private void TurnAndForce()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * turnSpeed);
        rb2D.AddForce(transform.right * flightSpeed);
    }

    private IEnumerator StartMissle ()
    {
        yield return new WaitUntil(() => missleParamsLoaded == true);

        StartCoroutine(RunMissleLifeSpan());
    }

    private IEnumerator RunMissleLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);

        isActive = false;
    }

    private IEnumerator MissleFade()
    {
        transform.DetachChildren();
        flightSpeed = fadeFlightSpeed;

        for (float scale = 1; scale > 0; scale -= fadeSpeed)
        {
            transform.localScale = new Vector3(scale, scale, 0);
            yield return null;
        }

        Destroy(gameObject);
    }

    //Collisions
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Missle")
        {
            smokeTrail.transform.parent = null;
            GameObject explosion = Instantiate(explosionController, gameObject.transform.position, Quaternion.identity);
            explosion.GetComponent<ExplosionController>().MissleToMissle();
            transform.DetachChildren();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Airplane")
        {
            //explosionController.MissleToAirplane();

            // TODO add points to gamePlay total points

            gamePlay.ShowResetUI();
        }
    }


}
