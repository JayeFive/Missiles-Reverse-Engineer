using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Game Loop
    private string[] initialScenesToLoad = { "UI", "WorldScene", "GamePlay" };

    // Persistence 
    public int playerScore = 0;

    // UI hooks



    // GameObject hooks
    private Airplane _airplane;
    public Airplane Airplane { get; set; }

    private UIManager _UIManager;
    public UIManager UIManager
    {
        get { return _UIManager; }
        set { _UIManager = value; }
    }


    private TouchJoystick _joystick = null;


    //MonoBehavior
    void Awake()
    {
        foreach(string sceneName in initialScenesToLoad)
        {
            RestartScene(sceneName);
        }
        
        StartCoroutine(SetPrimaryScene("WorldScene"));
    }


    // Scene Management
    private void RestartScene(string sceneName)
    {
        var _scene = SceneManager.GetSceneByName(sceneName);

        if (_scene.isLoaded == true)
        {
            SceneManager.UnloadSceneAsync(_scene);
            StartCoroutine(ReloadSceneAfterUnloading(sceneName));
        }
        else
        {
            LoadSceneAdditively(sceneName);
        }
    }

    private IEnumerator ReloadSceneAfterUnloading (string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded == true)
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            LoadSceneAdditively(sceneName);
        }
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
    private void LoadHomeScreen()
    {

    }

    private void LoadHud()
    {
        
    }

    private void LoadGamePlay()
    {
        LoadSceneAdditively("GamePlay");
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

    public void UpdateStarScore(int value)
    {
        if (_UIManager != null)
        {
            _UIManager.DisplayStarScore(value);
        }
    }
}
