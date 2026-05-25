using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collect"))
        {
            HealthSystem health = other.GetComponentInParent<HealthSystem>();

            if (health != null)
            {
                health.Heal(healAmount);
            }

            Destroy(gameObject);
        }
    }
}