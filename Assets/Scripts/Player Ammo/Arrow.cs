using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private AmmoData data;

    public float lifetime = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = direction.normalized * data.speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Debug.Log("Hit enemy with " + data.ammoType);

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