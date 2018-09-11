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
    private List<GameObject> currentSceneArrangements = new List<GameObject>();

    void Start ()
    {
        if (arrangements.Length > 0)
        {
            type = arrangements[0].gameObject.tag;
        } 
        else
        {
            type = "No arrangements found";
        }

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
            Instantiate(arrangementToSpawn, GetSpawnLoc(), Quaternion.identity);
            currentSceneArrangements.Add(arrangementToSpawn);
        }

        StartCoroutine(Spawn());
    }
    
    // Determine spawnable arrangement list
    private List<Arrangement> SpawnableArrangements ()
    {
        List<Arrangement> spawnableArrangements = new List<Arrangement>();
        var currentSceneWeight = CurrentSceneWeight();

        foreach (Arrangement arrangement in arrangements)
        {
            if (arrangement.SpawnWeight < maxSceneWeight - currentSceneWeight)
            {
                spawnableArrangements.Add(arrangement);
            }
        }

        return spawnableArrangements;
    }

    private float CurrentSceneWeight ()
    {
        int currentSceneWeight = 0;
        Arrangement[] sceneArrangements = FindObjectsOfType<Arrangement>();

        foreach(Arrangement sceneArrangement in sceneArrangements)
        {
            if (sceneArrangement.gameObject.tag == type)
            {
                currentSceneWeight += sceneArrangement.SpawnWeight;
            }
        }

        return currentSceneWeight;
    }


    // Location determination to be moved later
    // For testing, spawns center screen
    private Vector2 GetSpawnLoc()
    {
        return Camera.main.transform.position;
    }
}
