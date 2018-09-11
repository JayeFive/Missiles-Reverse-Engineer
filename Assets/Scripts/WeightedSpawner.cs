using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeightedSpawner
{
    public static GameObject GetChanceArrangement (IList<Arrangement> arrangements)
    {
        float totalWeight = TotalWeight(arrangements);
        SetWeightRanges(arrangements, totalWeight);

        return ChanceArrangement(arrangements, totalWeight);
    }

    private static float TotalWeight(IList<Arrangement> arrangements)
    {
        float totalWeight = 0.0f;

        foreach (Arrangement arrangement in arrangements)
        {
            totalWeight += arrangement.FractionalWeight;
        }

        return totalWeight;
    }

    private static void SetWeightRanges(IList<Arrangement> arrangements, float totalWeight)
    {
        float lastMax = 0.0f; 
        
        foreach (Arrangement arrangement in arrangements)
        {
            arrangement.ChanceMin = lastMax;
            arrangement.ChanceMax = arrangement.ChanceMin + arrangement.FractionalWeight;
            lastMax = arrangement.ChanceMax;
        }
    }

    private static GameObject ChanceArrangement(IList<Arrangement> arrangements, float totalWeight)
    {
        float rng = Random.Range(0.0f, totalWeight);

        foreach (Arrangement arrangement in arrangements)
        {
            if (rng >= arrangement.ChanceMin && rng < arrangement.ChanceMax)
            {
                return arrangement.gameObject;
            }
        }

        Debug.Log("Weighted spawner returned null!"); // impossible to reach
        return null;
    }
}