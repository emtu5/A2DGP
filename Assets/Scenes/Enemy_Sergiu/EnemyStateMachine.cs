using UnityEngine;
using System.Collections;
public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;
    public BulletSpawner bulletSpawner;
    private Rigidbody2D rb2D; 
    private SpriteRenderer spriteRenderer;
    public Transform player;

    [Header("Teleport Bounds")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;
    public Animator animator;
    private Coroutine flashRed;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
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

        public Vector2 GetRandomTeleportPosition()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }

     public System.Collections.IEnumerator TeleportFlash()
    {
        if (spriteRenderer == null) yield break;
        Color original = spriteRenderer.color;
        spriteRenderer.color =new Color(0.8f, 0.8f, 0.8f);
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = original;
    
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        if (currentState is MovingState movingState)
        {
            movingState.SetSpeedMultiplier(multiplier);
        }
    }

    public void SetFiringSpeedMultiplier(float multiplier)
    {
        if (currentState is ShootingState shootingState)
        {
            shootingState.SetFiringSpeedMultiplier(multiplier);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            Camera.main.GetComponent<CameraShake>().Shake(0.15f, 0.08f);
            SpriteRenderer enemySprite = GetComponent<SpriteRenderer>();
            if (enemySprite != null)
            {
                if (flashRed != null)
                {
                    StopCoroutine(flashRed);
                }
                flashRed = StartCoroutine(FlashRed(enemySprite));
            }
        }
    }

    private IEnumerator FlashRed(SpriteRenderer sprite)
    {
        Color originalColor = sprite.color;
        sprite.color = new Color(1f, 0.5f, 0.5f, originalColor.a);
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        flashRed = null;
    }
}