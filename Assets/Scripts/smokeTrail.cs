using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeTrail : MonoBehaviour {

	void Update ()
    {
	    if (transform.parent == null)
        {
            CheckForEnd();
        }	
	}

    private void CheckForEnd ()
    {
        if (gameObject.GetComponent<ParticleSystem>().particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
