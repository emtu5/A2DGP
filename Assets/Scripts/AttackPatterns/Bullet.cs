using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;
    private float acceleration = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Destroy(gameObject, life);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    internal void Init(Vector2 dir, float bulletSpeed, float bulletLifetime)
    {
        throw new NotImplementedException();
    }
}
