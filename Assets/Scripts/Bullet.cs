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
    }

    public void SetMoveDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
