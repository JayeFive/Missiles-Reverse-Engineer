using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Airplane airplane;
    private Vector3 offset;


    // MonoBehavior
    void Start()
    {
        StartCoroutine(FindAirplane());
    }

    void LateUpdate()
    {
        if (airplane != null)
        {
            transform.position = airplane.transform.position + offset;
        }
    }

    private IEnumerator FindAirplane()
    {
        airplane = GameManager.Instance.Airplane;
        
        while (airplane == null)
        {
            yield return new WaitForEndOfFrame();
            airplane = GameManager.Instance.Airplane;
        }

        offset = transform.position - airplane.transform.position;
    }
}