using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject airplane;
    private Vector3 offset;

    public GameObject testMissle;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - airplane.transform.position; // DEV TODO uncomment
        //offset = transform.position - testMissle.transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = airplane.transform.position + offset;  // DEV TODO uncomment
        //transform.position = testMissle.transform.position + offset;
    }
}
