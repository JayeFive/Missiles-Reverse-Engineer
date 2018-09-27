using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmokeTrail : MonoBehaviour {

    [SerializeField] public int burstCount;
    [SerializeField] float timeBetweenBursts;

    private ParticleSystem smokeParticles;
    private ParticleSystem.EmissionModule emissionModule;

    // Bursts

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
        emissionModule.rateOverDistance = 0;
        emissionModule.burstCount = burstCount;

        for (int i = 0; i < burstCount; i++)
        {
            var burst = new ParticleSystem.Burst(i * 0.1f, 1, 1, 5, timeBetweenBursts);
            emissionModule.SetBurst(i, burst);
        }
    }

    public void DisableBursts ()
    {
        for (int i = 0; i < burstCount; i++)
        {
            var burst = new ParticleSystem.Burst(0, 0);
            emissionModule.SetBurst(i, burst);
        }
    }

    private void CheckForEnd ()
    {
        if (smokeParticles.particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
