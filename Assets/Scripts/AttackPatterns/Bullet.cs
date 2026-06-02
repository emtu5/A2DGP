using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;
    private float acceleration = 0f;
    private float lifespan = 0f;
    private float timeLapsed = 0f;
    private float homingTimer = 0f;
    private bool isFadeOut = false;
    private PooledObject pooledObject;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;

    [Header("Damage Settings")]
    [SerializeField] private float damage = 10f;

    void Awake()
    {
        pooledObject = GetComponent<PooledObject>();
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
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
        if (isFadeOut)
        {
            spriteRenderer.color = new Color (
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                (lifespan - timeLapsed) / lifespan
            );
        }  
        timeLapsed += Time.deltaTime;
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

    public void SetFadeOut(bool fade)
    {
        isFadeOut = fade;
        spriteRenderer.color = Color.white;
        timeLapsed = 0f;
    }

    public void SetRadius(float radius)
    {
        collider.radius = radius;
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

            FindObjectOfType<AudioManager>()
                .PlaySFX(FindObjectOfType<AudioManager>().playerBulletHit);

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

    public void SetSprite(Sprite spr)
    {
        if (spr != null)
        {
            spriteRenderer.sprite = spr;
        }
    }

    internal void Init(Vector2 dir, float bulletSpeed, float bulletLifetime)
    {
        moveDirection = dir.normalized;
        moveSpeed = bulletSpeed;
        lifespan = bulletLifetime;

        StartDeactivating();
    }
}