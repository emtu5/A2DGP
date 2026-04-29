using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoType ammoType;
    public int amount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerAmmo player = collision.GetComponent<PlayerAmmo>();

        if (player == null)
            return;

        player.AddAmmoPickup(ammoType, amount);

        Destroy(gameObject);
    }
}