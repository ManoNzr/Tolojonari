using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        // Au lieu de lancer le jeu, on lance le menu !
        LoadMainMenu();

        // C'est ici plus tard que tu mettras ta ligne : 
        // SaveSystem.LoadData();
    }

    public void LoadMainMenu()
    {
        // On charge le menu principal
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    // Le bouton "Jouer" de ton menu principal appellera cette fonction !
    public void StartGame()
    {
        // 1. On enlève le menu principal de l'écran
        SceneManager.UnloadSceneAsync("MainMenu");

        // 2. On charge l'UI et le niveau en fond (Async évite que le jeu freeze)
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Stage 0", LoadSceneMode.Additive);
    }
}