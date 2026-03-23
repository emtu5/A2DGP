using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // refactor to ScriptableObject for bullet pattern
    private Vector3 startPoint;
    private Vector3 moveDirection;
    private float moveSpeed = 6f;
    private float acceleration = 0f;
    private float angle = 0f;
    private float firingRate;
    private float numberOfBullets = 6;
    [SerializeField]
    private GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPoint = transform.position;
        float angleSpacing = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Cos((angle + i * angleSpacing) * Mathf.PI / 180f);
            float bulletDirYPosition = startPoint.y + Mathf.Sin((angle + i * angleSpacing) * Mathf.PI / 180f);
            Vector3 newPositionVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
            Vector3 bulletDirection = (newPositionVector - startPoint).normalized;

            Bullet newBullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            newBullet.transform.position = startPoint;
            newBullet.SetMoveDirection(bulletDirection);
            newBullet.SetMoveSpeed(moveSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
