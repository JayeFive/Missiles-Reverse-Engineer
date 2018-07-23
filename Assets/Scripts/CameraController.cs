using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject airplane;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - airplane.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = airplane.transform.position + offset;
	}
}
