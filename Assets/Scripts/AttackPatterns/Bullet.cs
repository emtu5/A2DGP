using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;
    private float acceleration = 0f;
    private float lifespan = 0f;
    private PooledObject pooledObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    // Update is called once per frame
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
        throw new NotImplementedException();
    }
}
