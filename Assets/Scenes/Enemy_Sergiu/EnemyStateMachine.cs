using UnityEngine;

// ============ STATE INTERFACE ============
public interface IEnemyState
{
    void Enter(EnemyStateMachine enemy);
    void Update(EnemyStateMachine enemy);
    void Exit(EnemyStateMachine enemy);
}

// ============ MOVING STATE ============
public class MovingState : IEnemyState
{
    private Vector2 movementDirection;
    private float changeDirectionTimer;
    private float moveTimer;

    private float changeDirectionTime = 2f;
    private float moveDuration = 3f;   // how long enemy moves before shooting
    private float moveSpeed = 2f;

    public void Enter(EnemyStateMachine enemy)
    {
        ChooseNewDirection();
        changeDirectionTimer = changeDirectionTime;
        moveTimer = 0f;
        Debug.Log("Entered Moving State");
    }

    public void Update(EnemyStateMachine enemy)
    {
        // COUNT total time in moving state
        moveTimer += Time.deltaTime;

        // After some seconds → switch to shooting
        if (moveTimer >= moveDuration)
        {
            enemy.ChangeState(new ShootingState());
            return;
        }

        // Change direction occasionally
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChooseNewDirection();
        }

        // Move enemy
        enemy.transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }

    public void Exit(EnemyStateMachine enemy)
    {
        Debug.Log("Exited Moving State");
    }

    private void ChooseNewDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized;
        changeDirectionTimer = changeDirectionTime;
    }
}

// ============ SHOOTING STATE (STATIONARY) ============
public class ShootingState : IEnemyState
{
    private float shootTimer;
    private float timeInState;

    private float stateDuration = 3f;

    public void Enter(EnemyStateMachine enemy)
    {
        shootTimer = 0f;
        timeInState = 0f;
        Debug.Log("Entered Shooting State");
    }

    public void Update(EnemyStateMachine enemy)
    {
        // Count total time in shooting state
        timeInState += Time.deltaTime;

        // Leave shooting state after some seconds
        if (timeInState >= stateDuration)
        {
            enemy.ChangeState(new MovingState());
            return;
        }

        // Fire bullets based on pattern fire rate
        shootTimer += Time.deltaTime;

        if (shootTimer >= enemy.bulletSpawner.Pattern.firingRate)
        {
            enemy.bulletSpawner.FireBullets();
            shootTimer = 0f;
        }
    }

    public void Exit(EnemyStateMachine enemy)
    {
        Debug.Log("Exited Shooting State");
    }
}

// ============ ENEMY STATE MACHINE ============
public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;
    public BulletSpawner bulletSpawner;
    private Rigidbody2D rb;
    private Rigidbody2D rb2D; // For 2D physics
    
    void Start()
    {
        // Get the Rigidbody components (try both 2D and 3D)
        rb2D = GetComponent<Rigidbody2D>();
        
        // Find BulletSpawner if not assigned
        if (bulletSpawner == null)
        {
            bulletSpawner = GetComponent<BulletSpawner>();
        }
        
        if (bulletSpawner == null)
        {
            Debug.LogError("No BulletSpawner component found on " + gameObject.name + "! Please add a BulletSpawner component.");
            return;
        }
        
        if (bulletSpawner.Pattern == null)
        {
            Debug.LogError("No AttackPattern assigned to BulletSpawner on " + gameObject.name + "! Please assign a pattern in the Inspector.");
            return;
        }
        
        Debug.Log("EnemyStateMachine initialized successfully on " + gameObject.name);
        ChangeState(new MovingState());
    }
    
    void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this);
        }
    }
    
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }
        
        currentState = newState;
        currentState.Enter(this);
    }
    
    // Public method to enable/disable movement
    public void SetMovementEnabled(bool enabled)
    {
        // Disable Rigidbody movement
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
        
        if (rb2D != null)
        {
            rb2D.linearVelocity = Vector2.zero;
        }
        
        // Also stop any ongoing movement from the MovingState by resetting position delta
        if (!enabled)
        {
            // Force stop any transform movement
            transform.position = transform.position;
        }
        
        Debug.Log("Movement " + (enabled ? "ENABLED" : "DISABLED"));
    }
}