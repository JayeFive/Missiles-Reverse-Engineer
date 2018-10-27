using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance = null;
    public int playerScore = 0;

    public Scene UI;
    public Scene GamePlay;

    //MonoBehavior
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        RestartWorldScene();
        LoadHud();
        LoadGamePlay();

    }



    // Scene Management 
    private void RestartWorldScene()
    {
        LoadSceneAdditively("WorldScene");
    }

    private static void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    
    // UI Management
    private void LoadHomeScreen()
    {

    }

    private void LoadHud()
    {
        LoadSceneAdditively("UI");
        UI = SceneManager.GetSceneByName("UI");
    }

    private void LoadGamePlay()
    {
        LoadSceneAdditively("GamePlay");
        GamePlay = SceneManager.GetSceneByName("GamePlay");
    }

    private void LoadTouchControls()
    {
        LoadSceneAdditively("TouchControls");
    }

    private void TogglePauseButton()
    {

    }

    private void ToggleGameOverUI()
    {

    }
}
