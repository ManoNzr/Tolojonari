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
    }

    private void Die()
    {
        Debug.Log("Mario de papier est tout déchiré ! (Game Over)");

        gameObject.SetActive(false);
    }
}