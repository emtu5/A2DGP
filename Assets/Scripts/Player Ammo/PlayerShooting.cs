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
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;

            Vector2 dir = (mouse - transform.position).normalized;

            playerAmmo.Shoot(dir);
        }
    }
}