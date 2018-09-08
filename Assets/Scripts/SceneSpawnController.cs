using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnController : MonoBehaviour
{
    // Designer fields
    [SerializeField] private string type = "";
    [SerializeField] private int maxSceneWeight = 0;
    [SerializeField] private float spawnTimerBase = 0.0f;
    [SerializeField] private float spawnTimerVariation = 0.0f;
    [SerializeField] private Arrangement[] arrangements = new Arrangement[0];

    // Logic fields
    private int currentSceneWeight = 0;

    // MaxSceneWeight property for missile spawn weight growing over time
    public int MaxSceneWeight
    {
        get { return maxSceneWeight; }
        set { maxSceneWeight = value; }
    }

    void Start ()
    {
        StartCoroutine(Spawn());
    }

    // Spawning Coroutine
    private IEnumerator Spawn ()
    {
        yield return new WaitForSeconds(Random.Range(spawnTimerBase - spawnTimerVariation, spawnTimerBase + spawnTimerVariation));

        var spawnableArrangements = SpawnableArrangements();

        if (spawnableArrangements.Count > 0)
        {
            var arrangementToSpawn = WeightedSpawner.GetChanceArrangement(spawnableArrangements);
            Vector2 loc = GetSpawnLoc();
            Instantiate(arrangementToSpawn, loc, Quaternion.identity);
        }

        StartCoroutine(Spawn());
    }
    

    
    // Determine spawnable arrangement list
    private List<Arrangement> SpawnableArrangements ()
    {
        List<Arrangement> spawnableArrangements = new List<Arrangement>();

        foreach (Arrangement arrangement in arrangements)
        {
            if (arrangement.SpawnWeight < maxSceneWeight - currentSceneWeight)
            {
                spawnableArrangements.Add(arrangement);
            }
        }

        return spawnableArrangements;
    }



    // Location determination to be moved later
    // For testing, spawns center screen
    private Vector2 GetSpawnLoc()
    {
        return Camera.main.transform.position;
    }
}
