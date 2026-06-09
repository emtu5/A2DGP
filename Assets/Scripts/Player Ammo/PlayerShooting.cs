using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerAmmo playerAmmo;

    [Header("Shooting")]
    [SerializeField] private float cooldown = 0.2f;
    private float cooldownTimestamp;

    [Header("VFX")]
    [SerializeField] private GameObject shootVFXPrefab;
    [SerializeField] private Transform muzzlePoint;

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
        // cooldown check
        if (Time.time < cooldownTimestamp) return;
        cooldownTimestamp = Time.time + cooldown;

        // get mouse direction
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector2 dir = (mouse - transform.position).normalized;

        // recoil
        Camera.main.GetComponent<CameraRecoil>()
            .Recoil(dir * 0.01f, 0.2f);

        // VFX spawn
        if (shootVFXPrefab != null && muzzlePoint != null)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            Instantiate(shootVFXPrefab, muzzlePoint.position, rot);
        }

        // shooting logic
        playerAmmo.Shoot(dir);
    }
}