using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBonusArrangement : WeightedSpawnable {

    private int children = 0;

    void Start()
    {
        children = transform.childCount;
    }

    public int Children
    {
        get { return children; }
        set
        {
            children = value;
            if (children == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
