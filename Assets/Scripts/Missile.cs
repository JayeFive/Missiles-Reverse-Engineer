using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] private float flightSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float lifeSpan;
    [SerializeField] private float waitToSpawn;
    private Vector2 spawnVector;

    private GameObject explosionController;
    private GameObject smokeTrail;

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

    public float WaitToSpawn
    {
        get { return waitToSpawn; }
        set { waitToSpawn = value; }
    }

    public Vector2 SpawnLocation
    {
        get { return spawnVector; }
    }

    // Monobehavior
    void Start ()
    {
        spawnVector = Camera.main.transform.position - transform.position;
        explosionController = Resources.Load<GameObject>("Prefabs/ExplosionController");
        smokeTrail = (GameObject)Resources.Load("Prefabs/smokeTrail");
        Instantiate(smokeTrail, transform);
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
        GetComponent<MissileController>().isActive = false;
        smokeTrail.transform.parent = null;
        transform.DetachChildren();
        GetComponent<OffscreenIndicator>().DestroyIndicator();
        Destroy(GetComponent<OffscreenIndicator>());
    }

    public void DestroyMissile ()
    {
        GetComponentInParent<Arrangement>().Children--;
        Destroy(gameObject);
    }
}
