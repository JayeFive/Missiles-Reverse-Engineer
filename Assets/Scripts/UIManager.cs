using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas HUD;
    public Canvas HomeScreen;
    public Canvas GameOverScreen;


    // MonoBehavior
    void Awake()
    {
        GameManager.Instance.UIManager = this;
    }


    // UI Methods
    public void DisplayStarScore(int value)
    {
        var starScore = HUD.transform.Find("StarScoreText");

        if (starScore != null)
        {
            starScore.GetComponent<Text>().text = value.ToString();
        }
        else
        {
            Debug.LogWarning("[UIManager] StarScoreText not found in HUD");
        }
    }
}
