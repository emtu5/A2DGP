using System.Collections;
using UnityEngine;

public class MinionFollower : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stoppingDistance = 0.3f;   // stop when this close to player

    [Header("Animation")]
    public Animator animator;

    private bool isAttacking = false;
    [Header("Attack Data")]
    public float attackDamage = 3f;
    public float attackTimer = 0.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
            else Debug.LogError("Minion: Player not found!");
        }

        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position);
        float distance = direction.magnitude;

        // Only move if beyond stopping distance
        if (distance > stoppingDistance)
        {
            direction.Normalize();

            // Movement (same as enemy)
            if (rb != null)
                rb.linearVelocity = direction * moveSpeed;
            else
                transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Animation: set raw direction values (enemy style)
            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
        else
        {
            // Stop moving when close
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
            if (isAttacking == false)
            {
                Attack();
            }
        }
    }

    void OnDisable()
    {
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    void Attack()
    {
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        isAttacking = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();
        FreezeTime freezeTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<FreezeTime>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            if (freezeTime != null)
            {
                freezeTime.Freeze(0.05f);
            }
        }

        FindObjectOfType<AudioManager>()
            .PlaySFX(FindObjectOfType<AudioManager>().playerBulletHit);
        
        yield return new WaitForSeconds(attackTimer);
        isAttacking = false;
    }
}