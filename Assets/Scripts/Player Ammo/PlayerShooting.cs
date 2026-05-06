using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerAmmo playerAmmo;
    [SerializeField] private float cooldown = 0.2f;
    private float cooldownTimestamp;

    void Start()
    {
        playerAmmo = GetComponent<PlayerAmmo>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        // check if we passed the time the player should be able to shoot at, then update
        if (Time.time < cooldownTimestamp) return;
        cooldownTimestamp = Time.time + cooldown;

        // actually shooting
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector2 dir = (mouse - transform.position).normalized;

        playerAmmo.Shoot(dir);
    }
}