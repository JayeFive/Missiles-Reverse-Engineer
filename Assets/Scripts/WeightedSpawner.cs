using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeightedSpawner
{
    public static GameObject GetChanceObj (IList<SpawnWeight> objs)
    {
        int totalWeight = TotalWeight(objs);
        float totalPercentage = TotalPercentage(objs, totalWeight);
        SetWeightRanges(objs, totalWeight);

        return ChanceObj(objs, totalPercentage);
    }

    private static int TotalWeight(IList<SpawnWeight> objs)
    {
        int totalWeight = 0;

        foreach (SpawnWeight obj in objs)
        {
            totalWeight += obj.spawnWeight;
        }

        return totalWeight;
    }

    private static float TotalPercentage(IList<SpawnWeight> objs, int totalWeight)
    {
        float totalPercentage = 0.0f;

        foreach (SpawnWeight obj in objs)
        {
            totalPercentage += obj.SpawnPercentage;
        }

        return totalPercentage;
    }

    private static void SetWeightRanges(IList<SpawnWeight> objs, int totalWeight)
    {
        float lastMax = 0.0f;
        
        foreach (SpawnWeight obj in objs)
        {
            obj.SpawnPercentage = (float)obj.spawnWeight / totalWeight;
            obj.ChanceMin = lastMax;
            obj.ChanceMax = obj.ChanceMin + obj.SpawnPercentage;
            lastMax = obj.ChanceMax;
        }

    }

    private static GameObject ChanceObj(IList<SpawnWeight> objs, float totalPercentage)
    {
        float rng = Random.Range(0.0f, totalPercentage);

        foreach (SpawnWeight obj in objs)
        {
            if (rng >= obj.ChanceMin && rng < obj.ChanceMax)
            {
                return obj.gameObject;
            }
        }

        Debug.Log("Weighted spawner returned null!");
        return null;
    }
}