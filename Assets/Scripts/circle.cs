using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour {

    public bool isExplosion = false;
    public Vector3 cloudDrift;

    private void FixedUpdate ()
    {
        transform.position += cloudDrift;
    }

}
