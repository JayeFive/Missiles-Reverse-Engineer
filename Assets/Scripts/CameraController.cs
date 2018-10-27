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
        airplane = FindObjectOfType<Airplane>();
        airplane = GameManager.instance.GamePlay.Get

        offset = transform.position - airplane.transform.position;
    }

    void LateUpdate()
    {
        transform.position = airplane.transform.position + offset;
    }
}