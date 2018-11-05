using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] private int bonusValue = 0;
    [SerializeField] private float flightSpeed = 0f;
    [SerializeField] private float offscreenSpeedMod = 0f;
    [SerializeField] private bool isRepeatableSpeedZone = false;
    [SerializeField] private float turnSpeed = 0f;
    [SerializeField] private float lifeSpan = 0f;
    [SerializeField] private bool hasGuidance = true;

    [SerializeField] private float oscPrecision = 0f;
    [SerializeField] private float oscLength = 0f;
    [SerializeField] private float oscSpeed = 0f;
    private float degToAirplane;
    private float oscMag;
    private int oscDir = 1;

    [SerializeField] private float maxTiltAngle = 0.0f;

    [SerializeField] private float fadeSpeed = 1.0f;
    [SerializeField] private float sputterSeconds = 0.0f;
    private float fadeSpeedMod = 0.015f;

    private Rigidbody2D rb2D;
    private Transform sprite;
    private GameObject smokeTrail;
    private GameObject explosionController;
    private Airplane airplane;
    private GamePlay gamePlay;

    private bool isActive = true;
    private bool hasEnteredSpeedZone = false;

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

    public bool HasEnteredSpeedZone
    {
        set { hasEnteredSpeedZone = value; }
    }

    public bool IsRepeatableSpeedZone
    {
        get { return isRepeatableSpeedZone; }
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
            if (hasEnteredSpeedZone) offscreenSpeedMod = 1.0f; 

            MoveMissile();
        }
        else
        {
            rb2D.angularVelocity = 0.0f;
        }
    }

    // Missile Motion
    private void MoveMissile ()
    {
        if (hasGuidance)
        {
            DirectMissile();
        }

        rb2D.velocity = transform.right * flightSpeed * offscreenSpeedMod;
    }

    private void DirectMissile ()
    {
        Vector2 missileTarget = ((Vector2)airplane.transform.position - rb2D.position).normalized;
        degToAirplane = Vector3.Cross(missileTarget, transform.right).z * Mathf.Rad2Deg;

        if (Mathf.Abs(degToAirplane) < oscPrecision)
        {
            missileTarget = Oscillate(missileTarget);
            degToAirplane = Vector3.Cross(missileTarget, transform.right).z;
        }
        else
        {
            ResetOscillator();
        }

        rb2D.angularVelocity = (-turnSpeed * degToAirplane * Mathf.Deg2Rad);

        TiltMissile();
    }

    private void TiltMissile()
    {
        float tiltAroundZ = rb2D.angularVelocity * (maxTiltAngle / turnSpeed);
        Vector3 targetTilt = new Vector3(0, 0, tiltAroundZ);

        sprite.transform.localEulerAngles = targetTilt;
    }


    // Oscillator
    private Vector2 Oscillate (Vector2 directionToAirplane)
    {
        var distanceToAirplane = (transform.position - airplane.transform.position).magnitude;
        var oscMax = distanceToAirplane < 4 ? oscLength * distanceToAirplane / 6 : oscLength;
  
        OscillateCalc(oscMax);

        Vector2 perp = Vector2.Perpendicular(directionToAirplane).normalized;
        var finalVector = (perp * oscMag) + (Vector2)airplane.transform.position;

        return (finalVector - rb2D.position).normalized;
    }

    private void OscillateCalc (float oscMax)
    {
        if (oscDir * oscMag < oscMax)
        {
            oscMag += oscDir * oscSpeed / 100;
        }
        else
        {
            oscDir *= -1;
        }


    }

    private void ResetOscillator ()
    {
        if (degToAirplane < 0)
        {
            oscMag = oscLength;
            oscDir = -1;
        }
        else
        {
            oscMag = -oscLength;
            oscDir = 1;
        }
    }


    // Lifespan methods and coroutines
    private void LoadResources ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        airplane = FindObjectOfType<Airplane>();
        rb2D = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0);
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
        var missile = other.GetComponent<Missile>();
        var airplane = other.GetComponent<Airplane>();

        if (missile == null && airplane == null)
        {
            return;
        }

        if (missile != null && other.GetInstanceID() > GetInstanceID())
        {
            GameObject explosion = Instantiate(explosionController, gameObject.transform.position, Quaternion.identity);
            explosion.GetComponent<ExplosionController>().MissileToMissile(bonusValue);
            gamePlay.Bonus += bonusValue;
        }
        else if (airplane != null)
        {
            Debug.Log("Airplane Hit! Bonus points: " + gamePlay.Bonus);

            //gamePlay.ShowResetUI();
        }

        RemoveComponents();
        DestroyMissile();
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
        GetComponentInParent<Arrangement>().NumChildren--;
        Destroy(gameObject);
    }
}
