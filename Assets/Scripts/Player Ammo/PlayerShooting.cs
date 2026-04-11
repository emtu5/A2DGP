using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerAmmo playerAmmo;

    void Start()
    {
        playerAmmo = GetComponent<PlayerAmmo>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - playerAmmo.firePoint.position).normalized;

        playerAmmo.Shoot(direction);
    }
}