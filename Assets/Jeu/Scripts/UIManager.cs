using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hearts; // Assure-toi que tes cœurs sont dans l'ordre (0 = gauche, 3 = droite)

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
    public void UpdateHearts(int currentHealth)
    {
        // On parcourt TOUS les cœurs de ton tableau
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
}