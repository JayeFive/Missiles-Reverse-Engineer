using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrangement : MonoBehaviour {

    [SerializeField] private int spawnWeight = 0;
    private float fractionalWeight = 0.0f;
    private float chanceMin = 0.0f;
    private float chanceMax = 0.0f;
    private int children = 0;

    public int Children
    {
        get
        {
            return children;
        }
        set
        {
            children = value;
            if (children == 0)
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

    void Awake()
    {
        children = transform.childCount;
    }

}
