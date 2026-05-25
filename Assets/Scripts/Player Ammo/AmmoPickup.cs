using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoType ammoType;
    public int amount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Collect"))
            return;

        PlayerAmmo player = collision.GetComponentInParent<PlayerAmmo>();

        if (player == null)
            return;

        player.AddAmmoPickup(ammoType, amount);

        Destroy(gameObject);
    }
}