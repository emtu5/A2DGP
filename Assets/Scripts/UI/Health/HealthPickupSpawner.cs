using UnityEngine;

public class HealthPickupSpawner : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private GameObject healthPickupPrefab;

    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 10f;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPickup), spawnInterval, spawnInterval);
    }

    private void SpawnPickup()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(
            healthPickupPrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}