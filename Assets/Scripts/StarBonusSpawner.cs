using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBonusSpawner : MonoBehaviour {

    private Airplane airplane;
   
    public static StarBonusArrangement[] starBonusArrangements;

    [SerializeField] GameObject speedBonus;

    [SerializeField] Vector2 spawnLoc1;
    [SerializeField] Vector2 spawnLoc2;
    [SerializeField] Vector2 spawnLoc3;
    Vector2[] spawnLocs = new Vector2[3];

    [SerializeField] float starTimerMin = 0.0f;
    [SerializeField] float starTimerMax = 0.0f;

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
        yield return new WaitForSeconds(Random.Range(starTimerMin, starTimerMax));

        SpawnBonus(StarBonusArrangement());

        StartCoroutine(SpawnStars());
    }

    private GameObject StarBonusArrangement()
    {
        return WeightedSpawner.GetChanceObj(starBonusArrangements);
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
