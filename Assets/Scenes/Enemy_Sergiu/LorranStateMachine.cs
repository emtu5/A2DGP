using UnityEngine;
using System.Collections;

public class LorranStateMachine : MonoBehaviour, IEnemyStateMachine 
{
    [Header("Required Components")]
    public Animator animator { get; private set; }
    public BulletSpawner bulletSpawner { get; private set; }
    public Transform player;

    [Header("State Durations")]
    public float moveDuration = 4f;
    public float shootDuration = 3f;
    public float spawnDuration = 2f;
    public float dashDuration = 1f;

    [Header("Spawn Settings")]
    public GameObject minionPrefab;
    public int minionsToSpawn = 3;
    public float spawnRadius = 2f;

    [Header("Dash Settings")]
    public float dashSpeed = 15f;

    // Private
    private IEnemyState currentState;
    private float stateTimer;
    private float currentDuration;
    private System.Random rng = new System.Random();
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletSpawner = GetComponent<BulletSpawner>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (bulletSpawner == null || bulletSpawner.Pattern == null)
        {
            Debug.LogError("Boss2: BulletSpawner or Pattern missing!");
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
                return new MovingState(false);        // auto‑transition OFF
            case 1:
                currentDuration = shootDuration;
                return new ShootingState(false);      // auto‑transition OFF
            case 2:
                currentDuration = spawnDuration;
                return new SpawnMinionsState(minionPrefab, minionsToSpawn, spawnRadius);
            case 3:
                currentDuration = dashDuration;
                return new DashState(dashSpeed, player);
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
}
