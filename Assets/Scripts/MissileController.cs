using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

    [SerializeField] float fadeSpeed = 0.015f;
    [SerializeField] float sputterTime;

    private GamePlay gamePlay;
    private Airplane airplane;
    private Missile missile;
    private Rigidbody2D rb2D;

    private float flightSpeed;
    private float turnSpeed;
    private float lifeSpan;

    public bool isActive = true;

    void Start ()
    {
        LoadResources();
        StartMissile();

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

    private void LoadResources ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        airplane = FindObjectOfType<Airplane>();
        missile = GetComponent<Missile>();
        rb2D = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<OffscreenIndicator>();
        GetComponent<OffscreenIndicator>().indicatorSprite = (GameObject)Resources.Load("Prefabs/missileWarningIndicator");
    }

    private void StartMissile ()
    {
        LoadMissileSpecs();
        StartCoroutine(WaitToSpawn());
    }

    private void LoadMissileSpecs()
    {
        flightSpeed = missile.FlightSpeed;
        turnSpeed = missile.TurnSpeed;
        lifeSpan = missile.LifeSpan;
    }

    private IEnumerator WaitToSpawn ()
    {
        yield return new WaitForSeconds(missile.WaitToSpawn);

        StartCoroutine(RunMissileToSputter());
    }

    private IEnumerator RunMissileToSputter()
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

        StartCoroutine(MissileFade());
    }

    private IEnumerator MissileFade()
    {
        missile.RemoveComponents();

        for (float scale = 1; scale > 0; scale -= fadeSpeed)
        {
            transform.localScale = new Vector3(scale, scale, 0);
            yield return null;
        }

        missile.DestroyMissile();
    }
}
