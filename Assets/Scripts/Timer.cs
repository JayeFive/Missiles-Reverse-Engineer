using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float _currentTime;
    private float startTime;

    public float CurrentTime { get { return _currentTime; } }

	// MonoBehavior
	void Start ()
    {
        startTime = Time.time;
	}
	
	void Update ()
    {
        _currentTime = Time.time - startTime;

        string minutes = ((int)_currentTime / 60).ToString();
        string seconds = ((int)_currentTime % 60).ToString("D2");

        GetComponent<Text>().text = minutes + ":" + seconds;
	}
}
