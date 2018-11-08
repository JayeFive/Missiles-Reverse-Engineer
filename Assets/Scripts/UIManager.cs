using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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


    // Input methods
    public void GameBegin()
    {
        GameManager.Instance.GameBegin();
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

    public void ShowHomeScreen()
    {
        HomeScreen.enabled = true;
    }

    public void HideHomeScreen()
    {
        HomeScreen.enabled = false;
    }

    public void ShowHUD()
    {
        HUD.enabled = true;
    }

    public void HideHUD()
    {
        HUD.enabled = false;
    }

    public void ShowGameOverScreen()
    {
        GameOverScreen.enabled = true;
    }

    public void HideGameOverScreen()
    {
        GameOverScreen.enabled = false;
    }

    public void HideAll()
    {
        HUD.enabled = false;
        HomeScreen.enabled = false;
        //GameOverScreen.enabled = false;
    }
}
