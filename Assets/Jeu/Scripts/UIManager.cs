using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hearts;


    [SerializeField] private GameObject PauseUI;

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