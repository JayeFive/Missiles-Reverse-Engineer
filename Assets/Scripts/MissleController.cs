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

    private bool isActive = true;

    [SerializeField] float fadeSpeed = 0.015f;
    [SerializeField] float fadeSpeedModifier;
    [SerializeField] float sputterTime;
    private float fadeFlightSpeed;

    void Start ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        airplane = FindObjectOfType<Airplane>();
        smokeTrail = (GameObject)Resources.Load("Prefabs/smokeTrail");
        rb2D = GetComponent<Rigidbody2D>();
        explosionController = Resources.Load<GameObject>("Prefabs/ExplosionController");
        gameObject.AddComponent<OffscreenIndicator>();
        GetComponent<OffscreenIndicator>().indicatorSprite = (GameObject)Resources.Load("Prefabs/missleWarningIndicator");

        Instantiate(smokeTrail, transform);
        StartCoroutine(StartMissile());

        fadeFlightSpeed = flightSpeed * fadeSpeedModifier;

    }

	void FixedUpdate ()
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

    private IEnumerator StartMissile ()
    {
        yield return new WaitUntil(() => missleParamsLoaded == true);

        StartCoroutine(RunMissleToSputter());
    }

    private IEnumerator RunMissleToSputter()
    {
        yield return new WaitForSeconds(lifeSpan - sputterTime);

        StartCoroutine(SputterOut());
    }

    private IEnumerator SputterOut ()
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

        yield return new WaitForSeconds(sputterTime);

        StartCoroutine(MissleFade());
    }

    private IEnumerator MissleFade()
    {
        flightSpeed = fadeFlightSpeed;
        RemoveComponents();

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
            GameObject explosion = Instantiate(explosionController, gameObject.transform.position, Quaternion.identity);
            explosion.GetComponent<ExplosionController>().MissleToMissle();

            RemoveComponents();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Airplane")
        {
            //explosionController.MissleToAirplane();

            // TODO add points to gamePlay total points

            gamePlay.ShowResetUI();
        }
    }

    // Garbage
    private void RemoveComponents ()
    {
        isActive = false;
        smokeTrail.transform.parent = null;
        GetComponent<OffscreenIndicator>().DestroyIndicator();
        Destroy(GetComponent<OffscreenIndicator>());
    }
}
