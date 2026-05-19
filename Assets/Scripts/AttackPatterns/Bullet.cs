using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;
    private float acceleration = 0f;
    private float lifespan = 0f;
    private float homingTimer = 0f;
    private PooledObject pooledObject;
    private GameObject player;

    [Header("Damage Settings")]
    [SerializeField] private float damage = 10f;

    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        if (homingTimer > 0)
        {
            moveDirection = (player.transform.position - transform.position).normalized;
            transform.eulerAngles = new Vector3(0, 0, (float)(Mathf.Atan2(moveDirection.y, moveDirection.x) * 180f / Math.PI));
        }
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

    public void SetHomingTimer(float home)
    {
        homingTimer = home;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit by bullet. Damage: " + damage);

            HealthSystem playerHealth = collision.GetComponentInParent<HealthSystem>();
            FreezeTime freezeTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<FreezeTime>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                if (freezeTime != null)
                {
                    freezeTime.Freeze(0.05f);
                }
            }

            Deactivate();
        }
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

    public void StartHomingTimer()
    {
        StartCoroutine(DeactivateHoming());
    }

    IEnumerator DeactivateHoming()
    {
        yield return new WaitForSeconds(homingTimer);
        homingTimer = 0;
    }

    internal void Init(Vector2 dir, float bulletSpeed, float bulletLifetime)
    {
        moveDirection = dir.normalized;
        moveSpeed = bulletSpeed;
        lifespan = bulletLifetime;

        StartDeactivating();
    }
}