using UnityEngine;
public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;
    public BulletSpawner bulletSpawner;
    private Rigidbody2D rb2D; 
    
    void Start()
    {
        
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