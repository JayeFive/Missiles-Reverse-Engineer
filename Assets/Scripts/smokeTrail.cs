using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeTrail : MonoBehaviour {

    [SerializeField] float sputterCurveMax;

    private ParticleSystem smokeParticles;
    private ParticleSystem.EmissionModule emissionModule; 

    void Start ()
    {
        smokeParticles = GetComponent<ParticleSystem>();
        emissionModule = smokeParticles.emission;
    }

	void Update ()
    {
	    if (transform.parent == null)
        {
            CheckForEnd();
        }	
	}

    public void SputterTrail ()
    {
        emissionModule.rateOverDistance = new ParticleSystem.MinMaxCurve(0.0f, sputterCurveMax);
    }

    private void CheckForEnd ()
    {
        if (smokeParticles.particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
