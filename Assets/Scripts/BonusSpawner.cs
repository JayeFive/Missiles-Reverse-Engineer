using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    private Airplane airplane;
   
    public static StarBonusArrangement[] starBonusArrangements;
    float totalPercentage = 0.0f;

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
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private IEnumerator SpawnStars ()
    {
        yield return new WaitForSeconds(GetSpawnTimer(starTimerMin, starTimerMax));

        var arrangement = SelectStarArrangement().gameObject;
        SpawnBonus(arrangement);

        StartCoroutine(SpawnStars());
    }

    private GameObject SelectStarArrangement()
    {
        FindTotalWeight();
        SetWeightRanges();

        float selection = Random.Range(0.0f, totalPercentage);

        return FindSelection(selection);
    }

    private void FindTotalWeight ()
    {
        foreach (StarBonusArrangement arrangement in starBonusArrangements)
        {
            totalSpawnWeight += arrangement.spawnWeight;
        }
    }

    private void SetWeightRanges ()
    {
        float lastMax = 0.0f;

        foreach (StarBonusArrangement arrangement in starBonusArrangements)
        {
            arrangement.SpawnPercentage = (float)arrangement.spawnWeight / totalSpawnWeight;
            arrangement.ChanceMin = lastMax;
            arrangement.ChanceMax = arrangement.ChanceMin + arrangement.SpawnPercentage;
            lastMax = arrangement.ChanceMax;
            totalPercentage += arrangement.SpawnPercentage;
        }
    }

    private GameObject FindSelection (float selection)
    {
        foreach (StarBonusArrangement arrangement in starBonusArrangements)
        {
            if (selection >= arrangement.ChanceMin && selection < arrangement.ChanceMax)
            {
                return arrangement.gameObject;
            }
        }

        Debug.Log("No spawn arrangement selected!");
        return null;
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
