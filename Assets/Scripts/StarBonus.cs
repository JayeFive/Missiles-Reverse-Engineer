using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBonus : MonoBehaviour {

    private GamePlay gamePlay;

	// Use this for initialization
	void Start ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Airplane")
        {
            gamePlay.StarScore++;

            gameObject.GetComponentInParent<StarBonusArrangement>().Children--;
            Destroy(gameObject);
        }
    }
}
