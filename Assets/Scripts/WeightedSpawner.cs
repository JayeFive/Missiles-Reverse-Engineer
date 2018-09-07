using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeightedSpawner
{
    public static GameObject RunWeight (IList<WeightedSpawnable> objs)
    {
        int totalWeight = FindTotalWeight(objs);
        float totalPercentage = SetWeightRanges(objs, totalWeight);

        return FindSelection(objs, totalPercentage);
    }

    private static int FindTotalWeight(IList<WeightedSpawnable> objs)
    {
        int totalWeight = 0;

        foreach (WeightedSpawnable obj in objs)
        {
            totalWeight += obj.spawnWeight;
        }

        return totalWeight;
    }

    private static float SetWeightRanges(IList<WeightedSpawnable> objs, int totalWeight)
    {
        float totalPercentage = 0.0f;
        float lastMax = 0.0f;
        
        foreach (WeightedSpawnable obj in objs)
        {
            obj.SpawnPercentage = (float)obj.spawnWeight / totalWeight;
            obj.ChanceMin = lastMax;
            obj.ChanceMax = obj.ChanceMin + obj.SpawnPercentage;
            lastMax = obj.ChanceMax;
            totalPercentage += obj.SpawnPercentage;
        }

        return totalPercentage;
    }

    private static GameObject FindSelection(IList<WeightedSpawnable> objs, float totalPercentage)
    {
        float rng = Random.Range(0.0f, totalPercentage);

        foreach (WeightedSpawnable obj in objs)
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