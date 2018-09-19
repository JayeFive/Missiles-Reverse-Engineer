using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBonus : MonoBehaviour {

    private GamePlay gamePlay;

    void Start ()
    {
        gamePlay = FindObjectOfType<GamePlay>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Airplane")
        {
            gamePlay.StarScore++;

            GetComponentInParent<Arrangement>().Children--;
            Destroy(gameObject);
        }
    }
}
