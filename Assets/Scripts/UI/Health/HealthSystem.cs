using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Damage Settings")]
    [SerializeField] private float damageAmount = 10f;

    [Header("Debug Keys")]
    [SerializeField] private KeyCode damageKey = KeyCode.H;

    public event Action<float, float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    private void Update()
    {
        if (Input.GetKeyDown(damageKey))
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        

        NotifyHealthChanged();

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

        void Die()
    {
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene("LoseScene");   
        }
        else if (CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Win_Scene");   
        }
    }
}
