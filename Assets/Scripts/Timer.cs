using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float _currentTime;
    private float _startTime;
    private bool _timerStarted = false;

    public float CurrentTime { get { return _currentTime; } }


	// MonoBehavior
	void Update ()
    {
        if (_timerStarted)
        {
            _currentTime = Time.time - _startTime;

            string minutes = ((int)_currentTime / 60).ToString();
            string seconds = ((int)_currentTime % 60).ToString("D2");

            GetComponent<Text>().text = minutes + ":" + seconds;
        }
	}


    // Timer methods
    public void StartTimer()
    {
        _startTime = Time.time;
        _timerStarted = true;
    }
}
