using UnityEngine;
using System.Collections;

public class FinalBossStateMachine : MonoBehaviour, IEnemyStateMachine
{
    [Header("Components")]
    public Animator animator { get; private set; }
    public BulletSpawner bulletSpawner { get; private set; }
    public Transform player;

    [Header("State Durations")]
    public float moveDuration = 4f;
    public float shootDuration = 3f;
    public float spawnDuration = 2f;
    public float teleportDuration = 1f;

    [Header("Spawn Settings")]
    public GameObject minionPrefab;
    public int minionsToSpawn = 3;
    public float spawnRadius = 2f;

    [Header("Teleport Bounds")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    [Header("Teleport Visual")]
    public Color teleportFlashColor = new Color(0.8f, 0.8f, 0.8f);
    public float flashDuration = 0.3f;

    private IEnemyState currentState;
    private float stateTimer;
    private float currentDuration;
    private System.Random rng = new System.Random();
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bulletSpawner = GetComponent<BulletSpawner>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (bulletSpawner == null || bulletSpawner.Pattern == null)
        {
            Debug.LogError("FinalBoss: BulletSpawner or Pattern missing!");
            enabled = false;
            return;
        }

        ChangeState(GetRandomState());
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            ChangeState(GetRandomState());
        }
        currentState?.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
        stateTimer = currentDuration;
    }

    private IEnemyState GetRandomState()
    {
        int roll = rng.Next(4);
        switch (roll)
        {
            case 0:
                currentDuration = moveDuration;
                return new MovingState(false);
            case 1:
                currentDuration = shootDuration;
                return new ShootingState(false);
            case 2:
                currentDuration = spawnDuration;
                return new SpawnMinionsState(minionPrefab, minionsToSpawn, spawnRadius);
            case 3:
                currentDuration = teleportDuration;
                return new TeleportState(0.5f, minX, maxX, minY, maxY, teleportFlashColor, flashDuration);
            default:
                currentDuration = moveDuration;
                return new MovingState(false);
        }
    }

    // ---------- IEnemyStateMachine implementation ----------
    public void SetMovementEnabled(bool enabled)
    {
        if (rb != null)
        {
            if (!enabled) rb.linearVelocity = Vector2.zero;
            rb.isKinematic = !enabled;
        }
    }

    public void SetAnimationBool(string name, bool value) => animator?.SetBool(name, value);
    public void SetAnimationFloat(string name, float value) => animator?.SetFloat(name, value);
    public void SetAnimationTrigger(string name) => animator?.SetTrigger(name);

    // Added missing methods
    public void SetSpeedMultiplier(float multiplier)
    {
        if (currentState is MovingState movingState)
            movingState.SetSpeedMultiplier(multiplier);
    }

    public void SetFiringSpeedMultiplier(float multiplier)
    {
        if (currentState is ShootingState shootingState)
            shootingState.SetFiringSpeedMultiplier(multiplier);
    }

    // Helper for teleport flash
    public IEnumerator DoTeleportFlash()
    {
        if (spriteRenderer == null) yield break;
        Color original = spriteRenderer.color;
        spriteRenderer.color = teleportFlashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = original;
    }
}