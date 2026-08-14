using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hearts;


    [SerializeField] private GameObject PauseUI;

    [SerializeField] private GameObject deathScreen;

    public static UIManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        deathScreen.SetActive(false);
    }

    /*private void Start()
    {
        
    }*/


    public void OpenPauseUI()
    {
        PauseUI.SetActive(true);
    }

    public void CloseUI()
    {
        PauseUI.SetActive(false);
    }


    public void showDeathScreen()
    {
        deathScreen.SetActive(true);
    }


    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {

            if (i < currentHealth)
            {
                Hearts[i].SetActive(true);
            }
            else
            {
                Hearts[i].SetActive(false);
            }
        }
    }


    public void GoMainMenu()
    {
        GameManager.Instance.goMainMenu();
    }

}