using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private AmmoData data;
    private SpriteRenderer spriteRenderer;

    public float lifetime = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifetime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    public void Initialize(AmmoData ammoData, Vector2 direction)
    {
        data = ammoData;
        spriteRenderer.sprite = data.sprite;
        
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = direction.normalized * data.speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (!(collision.CompareTag("Enemy") || collision.CompareTag("Minion"))) return;

        Debug.Log("Hit enemy with " + data.ammoType);

        // PLAYER BULLET HIT SOUND
        FindObjectOfType<AudioManager>()
            .PlaySFX(FindObjectOfType<AudioManager>().enemyBulletHit);

        EnemyEffects effects = collision.GetComponent<EnemyEffects>();
        effects?.FlashRed();

        if (data.effect != null)
        {
            data.effect.Apply(collision.gameObject, data.damage);
        }

        HealthSystem enemySystem = collision.GetComponent<HealthSystem>();

        enemySystem?.TakeDamage(data.damage);

        ReturnToPool();
    }

    void ReturnToPool()
    {
        ArrowPool.Instance.ReturnArrow(gameObject);
    }
}