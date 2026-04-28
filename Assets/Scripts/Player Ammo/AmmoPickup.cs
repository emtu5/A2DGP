using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoData ammoData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerAmmo playerAmmo = collision.GetComponent<PlayerAmmo>();

        if (playerAmmo != null)
        {
            playerAmmo.ChangeState(new LimitedAmmoState(ammoData));
        }

        Destroy(gameObject);
    }
}