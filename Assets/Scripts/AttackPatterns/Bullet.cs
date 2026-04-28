using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;
    private float acceleration = 0f;
    private float lifespan = 0f;
    private PooledObject pooledObject;

    [Header("Damage Settings")]
    [SerializeField] private float damage = 10f;

    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    void Update()
    {
        transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        moveSpeed += acceleration * Time.deltaTime;
    }

    public void SetMoveDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetAcceleration(float acc)
    {
        acceleration = acc;
    }

    public void SetLifetime(float life)
    {
        lifespan = life;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit by bullet. Damage: " + damage);

            HealthSystem playerHealth = collision.GetComponentInParent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Deactivate();
            return;
        }

        Deactivate();
    }

    public void StartDeactivating()
    {
        StartCoroutine(DeactivateTimer());
    }

    IEnumerator DeactivateTimer()
    {
        yield return new WaitForSeconds(lifespan);
        Deactivate();
    }

    void Deactivate()
    {
        moveDirection = Vector3.zero;
        moveSpeed = 0;
        pooledObject.Release();
        gameObject.SetActive(false);
    }

    internal void Init(Vector2 dir, float bulletSpeed, float bulletLifetime)
    {
        moveDirection = dir.normalized;
        moveSpeed = bulletSpeed;
        lifespan = bulletLifetime;

        StartDeactivating();
    }
}