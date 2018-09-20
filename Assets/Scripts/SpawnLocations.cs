using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SpawnLocations
{
    public static Vector2 GetSpawnLocation (Arrangement arrangement, float spawnMagnitude)
    {
        switch (arrangement.tag)
        {
            case "StarBonus":
                return StarBonusLoc(spawnMagnitude);
            case "PowerUp":
                break;
            case "Missile":
                return Camera.main.transform.position;
            default:
                Debug.Log("arrangement tag not found in SpawnLocations.cs");
                break;
        }

        return new Vector2(0, 0);
    }

    private static Vector2 StarBonusLoc (float spawnMagnitutde)
    {
        var airplanePos = (Vector2)GameObject.FindObjectOfType<Airplane>().transform.position;

        var spawnLoc = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0.0f)).normalized;

        return airplanePos + (spawnLoc * spawnMagnitutde);
    }
}

