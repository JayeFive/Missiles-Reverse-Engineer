using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrangement : MonoBehaviour {

    [SerializeField] private int spawnWeight = 0;
    [SerializeField] private float[] spawnDelays = new float[0];
    private float fractionalWeight = 0.0f;
    private float chanceMin = 0.0f;
    private float chanceMax = 0.0f;
    private int numChildren = 0;

    public int NumChildren
    {
        get
        {
            return numChildren;
        }
        set
        {
            numChildren = value;
            if (numChildren == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public int SpawnWeight
    {
        get { return spawnWeight; }
        set { spawnWeight = value; }
    }

    public float FractionalWeight
    {
        get
        {
            fractionalWeight = 1 / (float)spawnWeight;
            return fractionalWeight;
        }
    }

    public float ChanceMin
    {
        get { return chanceMin; }
        set { chanceMin = value; }
    }

    public float ChanceMax
    {
        get { return chanceMax; }
        set { chanceMax = value; }
    }

    // MonoBehavior
    void Awake()
    {
        if (spawnDelays.Length > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (spawnDelays[i] > 0)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    StartCoroutine(TimedDelay(transform.GetChild(i), spawnDelays[i]));
                }
            }
        }

        numChildren = transform.childCount;
    }

    // Delay coroutines
    private IEnumerator TimedDelay(Transform child, float t)
    {
        yield return new WaitForSeconds(t);

        child.gameObject.SetActive(true);
    }
}
