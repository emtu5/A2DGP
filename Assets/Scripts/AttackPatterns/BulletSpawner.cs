using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // refactor to ScriptableObject for bullet pattern
    private Vector3 startPoint;
    private Vector3 moveDirection;
    private float angleSpacing;
    [SerializeField]
    private float currentAngle = 0f;
    private float timeBeforeShot = 0f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private AttackPattern pattern;
    [SerializeField]
    private ObjectPool bulletPool;

    void Awake()
    {
        startPoint = transform.position;
        angleSpacing = 360f / pattern.numberOfBullets;
        currentAngle = pattern.startingAngle;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FireBullets()
    {
        startPoint = transform.position;
        angleSpacing = 360f / pattern.numberOfBullets;
        for (int i = 0; i < pattern.numberOfBullets; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Cos((currentAngle + i * angleSpacing) * Mathf.PI / 180f);
            float bulletDirYPosition = startPoint.y + Mathf.Sin((currentAngle + i * angleSpacing) * Mathf.PI / 180f);
            Vector3 newPositionVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
            Vector3 bulletDirection = (newPositionVector - startPoint).normalized;

            // Bullet newBullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            GameObject bulletObj = bulletPool.GetPooledObject().gameObject;
            Bullet newBullet = bulletObj.GetComponent<Bullet>();
            newBullet.transform.position = startPoint;
            newBullet.SetMoveDirection(bulletDirection);
            newBullet.SetMoveSpeed(pattern.moveSpeed);
            newBullet.SetAcceleration(pattern.acceleration);
            newBullet.SetLifetime(pattern.lifetime);
            newBullet.StartDeactivating();
        }

        currentAngle = (360f + currentAngle + pattern.angleStep) % 360f;
    }


    // Update is called once per frame
    void Update()
    {
        timeBeforeShot += Time.deltaTime;
        if (timeBeforeShot >= pattern.firingRate)
        {
            FireBullets();
            timeBeforeShot = 0;
        }
    }

    public void SetAttackPattern(AttackPattern pat)
    {
        pattern = pat;
    }
}
