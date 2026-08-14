using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isPaused = false;

    private bool canPause = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadMainMenu();
        canPause = true;
    }

    public void PauseGame()
    {
        if (!canPause)
        {
            return;
        }
        if (!isPaused)
        {
            isPaused = true;
            UIManager.Instance.OpenPauseUI();
        }
    }

    public void setCanPause(bool value)
    {
        canPause = value;
    }

    public bool canHePause()
        { return canPause; }


    public void UnPauseGame()
    {
        if (isPaused) isPaused = false; UIManager.Instance.CloseUI();
    }

    public bool IsPaused
        { get { return isPaused; } }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }


    public void death()
    {
        canPause = false;
        isPaused = true;
        UIManager.Instance.showDeathScreen();

    }



    public void goMainMenu()
    {

        SceneManager.UnloadSceneAsync("Stage 0");
        SceneManager.UnloadSceneAsync("UI");

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);

    }


    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("MainMenu");

        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Stage 0", LoadSceneMode.Additive);
    }
}