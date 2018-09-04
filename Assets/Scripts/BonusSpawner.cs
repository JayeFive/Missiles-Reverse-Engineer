using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    private Airplane airplane;

    [SerializeField] GameObject starBonus;
    [SerializeField] GameObject starBonusDiamond;
    [SerializeField] GameObject starBonusRow;
    GameObject[] bonusArrangements = new GameObject[3];

    [SerializeField] Vector2 spawnLoc1;
    [SerializeField] Vector2 spawnLoc2;
    [SerializeField] Vector2 spawnLoc3;
    Vector2[] spawnLocs = new Vector2[3];

    [SerializeField] float timerMin = 0.0f;
    [SerializeField] float timerMax = 0.0f;

    // Use this for initialization
    void Start ()
    {
        airplane = FindObjectOfType<Airplane>();

        spawnLocs[0] = spawnLoc1;
        spawnLocs[1] = spawnLoc2;
        spawnLocs[2] = spawnLoc3;

        bonusArrangements[0] = starBonus;
        bonusArrangements[1] = starBonusDiamond;
        bonusArrangements[2] = starBonusRow;

        StartCoroutine(StartBonusSpawner());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private IEnumerator StartBonusSpawner ()
    {
        float spawnTimer = Random.Range(timerMin, timerMax);
        yield return new WaitForSeconds(spawnTimer);

        var bonus = bonusArrangements[Random.Range(0, bonusArrangements.Length)];
        SpawnBonusStar(bonus);

        StartCoroutine(StartBonusSpawner());
    }

    private void SpawnBonusStar (GameObject bonus)
    {
        Vector2 spawnLoc = GetSpawnLocation() + (Vector2)airplane.transform.position;
        Instantiate(bonus, spawnLoc, Quaternion.identity);
    }

    private Vector2 GetSpawnLocation ()
    {
        return spawnLocs[Random.Range(0, spawnLocs.Length)];
    }

}
