using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    [SerializeField] GameObject bonusStar;
    Camera camera;


	// Use this for initialization
	void Start ()
    {
        camera = FindObjectOfType<Camera>();

        var verticalExtent = camera.orthographicSize;
        var horizontalExtent = verticalExtent * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //private IEnumerator 

    private void SpawnBonusStar ()
    {
        Instantiate(bonusStar, GetSpawnLocation(), Quaternion.identity);
    }

    private Vector2 GetSpawnLocation ()
    {
        return new Vector2(Random.Range(-Screen.width - 2.0f, Screen.width + 2.0f), Random.Range(-2.0f, 0.0f));
    }

}
