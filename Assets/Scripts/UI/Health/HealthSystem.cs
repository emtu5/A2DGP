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

    [Header("VFX")]
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private Transform vfxSpawnPoint;

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

        //HIT VFX
        SpawnHitVFX();

        NotifyHealthChanged();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

        void Die()
    {
        // if (CompareTag("Player"))
        // {
        //     SceneManager.LoadScene("LoseScene");   
        // }
        // else if (CompareTag("Enemy"))
        // {
        //     SceneManager.LoadScene("Win_Scene");   
        // }
        // else if (CompareTag("Minion"))
        // {
        //     Destroy(gameObject);
        // }
        if (CompareTag("Minion"))
        {
            TutorialManager.Instance.MinionKilled();
            Destroy(gameObject);
        }
    }

    private void SpawnHitVFX()
    {
        if (hitVFXPrefab == null) return;

        Vector3 spawnPos = vfxSpawnPoint != null ? vfxSpawnPoint.position : transform.position;

        Instantiate(hitVFXPrefab, spawnPos, Quaternion.identity);
    }
}
