using System.Collections;
using UnityEngine;

public class LoadInit : MonoBehaviour
{
    public GameObject gameManager;

    //MonoBehavior
    void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
