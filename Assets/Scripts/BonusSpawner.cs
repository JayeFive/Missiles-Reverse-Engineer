using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    private Airplane airplane;
   
    public static StarBonusArrangement[] starBonusArrangements;

    [SerializeField] GameObject shieldBonus;
    [SerializeField] GameObject speedBonus;

    [SerializeField] Vector2 spawnLoc1;
    [SerializeField] Vector2 spawnLoc2;
    [SerializeField] Vector2 spawnLoc3;
    Vector2[] spawnLocs = new Vector2[3];

    [SerializeField] float starTimerMin = 0.0f;
    [SerializeField] float starTimerMax = 0.0f;
    [SerializeField] float powerUpTimerMin = 0.0f;
    [SerializeField] float powerUpTimerMax = 0.0f;

    private int totalSpawnWeight = 0;
    private float totalPercentage = 0.0f;

    // Use this for initialization
    void Start ()
    {
        airplane = FindObjectOfType<Airplane>();

        spawnLocs[0] = spawnLoc1;
        spawnLocs[1] = spawnLoc2;
        spawnLocs[2] = spawnLoc3;

        starBonusArrangements = Resources.FindObjectsOfTypeAll(typeof(StarBonusArrangement)) as StarBonusArrangement[];

        StartCoroutine(SpawnStars());
    }
	
    private IEnumerator SpawnStars ()
    {
        yield return new WaitForSeconds(GetSpawnTimer(starTimerMin, starTimerMax));

        SpawnBonus(SelectStarArrangement());

        StartCoroutine(SpawnStars());
    }

    private GameObject SelectStarArrangement()
    {
        return WeightedSpawner.RunWeight(starBonusArrangements);
    }

    private IEnumerator SpawnPowerUps ()
    {
        yield return new WaitForSeconds(GetSpawnTimer(powerUpTimerMin, powerUpTimerMax));


        // Decide if sheild either exists to pick up or is active on the airplane

        // Spawn the speed bonus if either is true
        // Spawn the shield if both are false

        // Set a lifespan timer on the powerup

        // Destroy the powerup if it's not gathered in time


        //SpawnBonus();


    }

    private float GetSpawnTimer (float min, float max)
    {
        return Random.Range(min, max);
    }

    private void SpawnBonus (GameObject bonus)
    {
        Vector2 spawnLoc = GetSpawnLocation() + (Vector2)airplane.transform.position;
        Instantiate(bonus, spawnLoc, Quaternion.identity);
    }

    private Vector2 GetSpawnLocation ()
    {
        return spawnLocs[Random.Range(0, spawnLocs.Length)];
    }
}
