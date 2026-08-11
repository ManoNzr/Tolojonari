using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isPaused = false;

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

    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            UIManager.Instance.OpenPauseUI();
        }
    }

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