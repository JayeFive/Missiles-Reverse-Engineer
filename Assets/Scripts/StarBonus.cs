using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBonus : MonoBehaviour {

    private Text starScore;
    private GamePlay gamePlay;

	// Use this for initialization
	void Start ()
    {
        starScore = GameObject.Find("StarCounterText").GetComponent<Text>();
        gamePlay = FindObjectOfType<GamePlay>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Airplane")
        {
            gamePlay.starScore++;
            starScore.GetComponent<Text>().text = gamePlay.starScore.ToString();

            gameObject.GetComponentInParent<StarBonusArrangement>().NumChildren--;
            Destroy(gameObject);
        }
    }
}
