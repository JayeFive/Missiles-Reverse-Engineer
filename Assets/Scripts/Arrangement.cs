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
            for (int childIndex = 0; childIndex < spawnDelays.Length; childIndex++)
            {
                CheckForDelay(childIndex);
            }
        }

        numChildren = transform.childCount;
    }

    private void CheckForDelay (int childIndex)
    {
        if (spawnDelays[childIndex] > 0)
        {
            DelaySpawning(childIndex);
        }
    }

    private void DelaySpawning (int childIndex)
    {
        var child = transform.GetChild(childIndex);
        var spawnVector = child.transform.position - Camera.main.transform.position;
        child.gameObject.SetActive(false);

        StartCoroutine(TimedDelay(transform.GetChild(childIndex), spawnVector, spawnDelays[childIndex]));
    }

    // Delay coroutines
    private IEnumerator TimedDelay(Transform child, Vector3 spawnVector, float t)
    {
        yield return new WaitForSeconds(t);

        child.transform.position = Camera.main.transform.position + spawnVector;
        child.gameObject.SetActive(true);
    }
}
