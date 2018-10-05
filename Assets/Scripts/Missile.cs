using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] private float flightSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float lifeSpan;

    [SerializeField] private float oscillateLength;
    [SerializeField] private float oscillateSpeed;
    private float oscMag = 0.0f;
    private int oscDir = 1;


    [SerializeField] private float fadeSpeed = 1.0f;
    [SerializeField] private float sputterSeconds = 0.0f;
    private float fadeSpeedMod = 0.015f;

    private Rigidbody2D rb2D;
    private GameObject smokeTrail;
    private GameObject explosionController;
    private Airplane airplane;
    private GamePlay gamePlay;

    private bool isActive = true;

    public float FlightSpeed
    {
        get { return flightSpeed; }
        set { flightSpeed = value; }
    }

    public float TurnSpeed
    {
        get { return turnSpeed; }
        set { turnSpeed = value; }
    }

    public float LifeSpan
    {
        get { return lifeSpan; }
        set { lifeSpan = value; }
    }


    // MonoBehavior
    void Start ()
    {
        explosionController = Resources.Load<GameObject>("Prefabs/ExplosionController");
        smokeTrail = (GameObject)Resources.Load("Prefabs/smokeTrail");
        smokeTrail = Instantiate(smokeTrail, transform);

        LoadResources();
        StartMissile();
    }

    void FixedUpdate ()
    {
        if (isActive)
        {
            Vector2 missileTarget = Oscillate(((Vector2)airplane.transform.position - rb2D.position));

            float rotateAmount = Vector3.Cross(missileTarget, transform.right).z;
            rb2D.angularVelocity = (-turnSpeed * rotateAmount);
            rb2D.velocity = transform.right * flightSpeed;
        }
        else
        {
            rb2D.angularVelocity = 0.0f;
        }
    }


    // Oscillator
    private Vector2 Oscillate (Vector2 directionToAirplane)
    {
        OscillateCalc();

        Vector2 perp = Vector2.Perpendicular(directionToAirplane).normalized;
        var finalVector = (perp * oscMag) + (Vector2)airplane.transform.position;

        return (finalVector - rb2D.position).normalized;
    }

    private void OscillateCalc ()
    {

        if (oscDir * oscMag < oscillateLength)
        {
            oscMag += oscDir * oscillateSpeed / 100;
        }
        else
        {
            oscDir *= -1;
        }
    }


    // Lifespan methods and coroutines
    private void LoadResources ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        airplane = FindObjectOfType<Airplane>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void StartMissile ()
    {
        StartCoroutine(RunMissileToSputter());
    }

    private IEnumerator RunMissileToSputter()
    {
        yield return new WaitForSeconds(lifeSpan - sputterSeconds);

        StartCoroutine(SputterOut());
    }

    private IEnumerator SputterOut ()
    {
        var smokeTrail = transform.Find("smokeTrail(Clone)").GetComponent<SmokeTrail>();

        smokeTrail.SputterTrail();

        yield return new WaitForSeconds(sputterSeconds / 2);
        smokeTrail.burstCount--;
        smokeTrail.SputterTrail();

        yield return new WaitForSeconds(sputterSeconds / 2);
        smokeTrail.DisableBursts();
        StartCoroutine(MissileFade());
    }

    private IEnumerator MissileFade ()
    {
        RemoveComponents();
        var _fadeSpeed = fadeSpeed * fadeSpeedMod;

        for (float scale = 1; scale > 0; scale -= _fadeSpeed)
        {
            transform.localScale = new Vector3(scale, scale, 0);
            yield return null;
        }

        DestroyMissile();
    }

    //Collisions
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Missile")
        {
            GameObject explosion = Instantiate(explosionController, gameObject.transform.position, Quaternion.identity);
            explosion.GetComponent<ExplosionController>().MissileToMissile();

            RemoveComponents();
            DestroyMissile();
        }
        else if (other.gameObject.tag == "Airplane")
        {
            //explosionController.MissleToAirplane();

            // TODO add points to gamePlay total points

            //gamePlay.ShowResetUI();
        }
    }

    // Garbage
    public void RemoveComponents ()
    {
        isActive = false;
        smokeTrail.GetComponent<SmokeTrail>().DisableBursts();
        smokeTrail.transform.parent = null;
        transform.DetachChildren();
        GetComponent<OffscreenIndicator>().DestroyIndicator();
        Destroy(GetComponent<OffscreenIndicator>());
    }

    public void DestroyMissile ()
    {
        UpdateArrangement();
        Destroy(gameObject);
    }

    private void UpdateArrangement ()
    {
        if(--GetComponentInParent<Arrangement>().NumChildren == 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
