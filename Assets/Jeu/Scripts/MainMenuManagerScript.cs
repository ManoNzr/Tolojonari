using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManagerScript : MonoBehaviour
{
    // Main Menu Manager, les interactions, les boutons, et etc, dans le menu principal quoi.. bref
    [Tooltip("Le nom de la scene qui se lance par défaut quand on clique sur le bouton New Game.")]
    [SerializeField] string newGameScene; // le nom de la scene par défaut..

    public void StartNewGame()
    {
        GameManager.Instance.StartGame();
        Debug.Log("Lancement d'un nouveau jeu..");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Le joueur a quitter le jeu là");
        return;
    }
}
