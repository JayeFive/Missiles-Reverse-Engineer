using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float currentTime;
    public Text timerText;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time - startTime;

        string minutes = ((int)currentTime / 60).ToString();
        string seconds = ((int)currentTime % 60).ToString("D2");

        timerText.text = minutes + ":" + seconds;
	}
}
