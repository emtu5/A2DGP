using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathAnim : MonoBehaviour
{
    [SerializeField] private HealthSystem targetHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private string sceneOnDeath;

    [SerializeField] private FadeEffect fadeEffect;
    [SerializeField] private float deathDelay = 2f;

    private bool isDead = false;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        targetHealth.OnHealthChanged += OnDeath;
    }

    private void OnDisable()
    {
        targetHealth.OnHealthChanged -= OnDeath;
    }

    void OnDeath(float current, float max)
    {
        if (current <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        // 1. Stop all movement (Rigidbody)
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;       
            rb.simulated = false;         
        }

        
        if (col != null) col.enabled = false;

       
        EnemyStateMachine stateMachine = GetComponent<EnemyStateMachine>();
        if (stateMachine != null) stateMachine.enabled = false;

        BulletSpawner bulletSpawner = GetComponent<BulletSpawner>();
        if (bulletSpawner != null) bulletSpawner.enabled = false;

        MonoBehaviour[] allScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (script != this && script != targetHealth) // keep health and this script active
                script.enabled = false;
        }

      
        animator.SetBool("IsDead", true);

      
        yield return new WaitForSeconds(deathDelay);

        if (fadeEffect != null) yield return StartCoroutine(fadeEffect.FadeIn(1f));

        SceneManager.LoadScene(sceneOnDeath);
    }
}