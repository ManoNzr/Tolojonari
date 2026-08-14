using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Paramètres de vie")]
    public int maxHealth = 4;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHearts(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHearts(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if(!GameManager.Instance.canHePause())
            {
                return; 
            }

            if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.UnPauseGame();
            }
            else
            {
                GameManager.Instance.PauseGame();
            }

        }
    }

    private void Die()
    {
        GameManager.Instance.death();

        gameObject.SetActive(false);
    }
}