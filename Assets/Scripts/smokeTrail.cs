using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeTrail : MonoBehaviour {

    private ParticleSystem ps;

    void Start ()
    {
        ps = GetComponent<ParticleSystem>();
    }

	void Update ()
    {
	    if (transform.parent == null)
        {
            CheckForEnd();
        }	
	}

    private void CheckForEnd ()
    {
        if (ps.particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
