using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Game Loop
    public bool gameIsLive = false;

    // Persistence 
    public int playerScore = 0;

    // GameObject hooks
    public Airplane Airplane;
    public UIManager UIManager;


    //MonoBehavior
    void Awake()
    {
        RestartScene("UI");
        StartCoroutine(WaitForUIManager());
    }
    

    // Initial game load
    private IEnumerator WaitForUIManager()
    {
        while (UIManager == null)
        {
            yield return new WaitForEndOfFrame();
        }

        LoadHomeScreen();
    }

    private void LoadHomeScreen()
    {
        RefreshUI();
        RestartScene("WorldScene", true);
        UIManager.ShowHomeScreen();
    }


    // Game Loop Management
    public void GameBegin()
    {
        UIManager.HideHomeScreen();
        StartCoroutine(Airplane.StartingTurn());
    }

    public void PlayerStart()
    {
        RestartScene("GamePlay");
        UIManager.ShowHUD();
        UIManager.HUD.GetComponentInChildren<Timer>().StartTimer();
    }

    // Scene Management
    private void RestartScene(string sceneName, bool setPrimary = false)
    {
        var _scene = SceneManager.GetSceneByName(sceneName);

        if (_scene.isLoaded == true)
        {
            SceneManager.UnloadSceneAsync(_scene);
            StartCoroutine(WaitToUnload(sceneName, setPrimary));
        }
        else
        {
            LoadScene(sceneName, setPrimary);
        }
    }

    private IEnumerator WaitToUnload (string sceneName, bool setPrimary)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded == true)
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            LoadScene(sceneName, setPrimary);
        }
    }

    private void LoadScene(string sceneName, bool setPrimary = false)
    {
        LoadSceneAdditively(sceneName);
        if (setPrimary) StartCoroutine(SetPrimaryScene(sceneName));
    }

    private void LoadUI()
    {
        LoadSceneAdditively("UI");
    }   

    private static void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private IEnumerator SetPrimaryScene(string sceneName)
    {
        yield return new WaitForEndOfFrame();

        var scene = SceneManager.GetSceneByName(sceneName);

        if (scene.isLoaded)
        {
            SceneManager.SetActiveScene(scene);
        }
        else
        {
            StartCoroutine(SetPrimaryScene(sceneName));
        }
    }

    
    // UI Management
    private void RefreshUI()
    {
        if (UIManager != null)
        {
            UIManager.HideAll();
        }
        else
        {
            Debug.LogWarning("[GameManager] Can't refresh UI, no UIManager found!");
        }

    }

    public void UpdateStarScore(int value)
    {
        if (UIManager != null)
        {
            UIManager.DisplayStarScore(value);
        }
    }
}
