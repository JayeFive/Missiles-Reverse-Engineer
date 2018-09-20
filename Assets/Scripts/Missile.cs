using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] private float flightSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float lifeSpan;

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
        Instantiate(smokeTrail, transform);

        LoadResources();
        StartMissile();
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 direction = ((Vector2)airplane.transform.position - rb2D.position).normalized;
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            rb2D.angularVelocity = (-turnSpeed * rotateAmount);
            rb2D.velocity = transform.right * flightSpeed;
        }
        else
        {
            rb2D.angularVelocity = 0.0f;
        }
    }

    // Lifespan methods and coroutines
    private void LoadResources()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        airplane = FindObjectOfType<Airplane>();
        rb2D = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<OffscreenIndicator>();
        GetComponent<OffscreenIndicator>().indicatorSprite = (GameObject)Resources.Load("Prefabs/missileWarningIndicator");
    }

    private void StartMissile()
    {
        StartCoroutine(RunMissileToSputter());
    }

    private IEnumerator RunMissileToSputter()
    {
        yield return new WaitForSeconds(lifeSpan - sputterSeconds);

        StartCoroutine(SputterOut());
    }

    private IEnumerator SputterOut()
    {
        var smokeTrail = transform.Find("smokeTrail(Clone)");

        if (smokeTrail != null)
        {
            smokeTrail.GetComponent<smokeTrail>().SputterTrail();
        }
        else
        {
            Debug.Log("Smoke trail not found!");
        }

        yield return new WaitForSeconds(sputterSeconds);

        StartCoroutine(MissileFade());
    }

    private IEnumerator MissileFade()
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
    private void OnTriggerEnter2D(Collider2D other)
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
    public void RemoveComponents()
    {
        isActive = false;
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
