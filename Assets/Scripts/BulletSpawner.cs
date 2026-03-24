using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // refactor to ScriptableObject for bullet pattern
    private Vector3 startPoint;
    private Vector3 moveDirection;
    [SerializeField]
    private float moveSpeed = 6f;
    private float acceleration = 0f;
    [SerializeField]
    private float angle = 0f;
    [SerializeField]
    private float angleStep = 0f;
    [SerializeField]
    private float firingRate = 1f;
    [SerializeField]
    private float numberOfBullets = 6;
    private float angleSpacing;
    private float horizontalAngle = 0f;
    private float timeBeforeShot = 0f;
    [SerializeField]
    private GameObject bulletPrefab;

    void Awake()
    {
        startPoint = transform.position;
        angleSpacing = 360f / numberOfBullets;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FireBullets()
    {
        angleSpacing = 360f / numberOfBullets;
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

        angle += angleStep;
        if (angle >= 360f || angle <= -360f)
            angle = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        timeBeforeShot += Time.deltaTime;
        if (timeBeforeShot >= firingRate)
        {
            FireBullets();
            timeBeforeShot = 0;
        }
    }
}
