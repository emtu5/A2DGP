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
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy with " + data.ammoType + " arrow");
            Debug.Log("Damage: " + data.damage);

            switch (data.ammoType)
            {
                case AmmoType.Fire:
                    Debug.Log("Fire Effect: High damage applied");
                    break;

                case AmmoType.Poison:
                    Debug.Log("Poison Effect: Damage over time applied");
                    break;

                case AmmoType.Ice:
                    Debug.Log("Ice Effect: Enemy slowed");
                    break;

                case AmmoType.Default:
                    Debug.Log("Default arrow: Normal damage");
                    break;

                default:
                    Debug.Log("Unknown ammo type");
                    break;
            }

            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        ArrowPool.Instance.ReturnArrow(gameObject);
    }
}