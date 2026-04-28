using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPickup), 1f, spawnInterval);
    }

    void SpawnPickup()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );

        int index = Random.Range(0, pickupPrefabs.Length);

        Instantiate(pickupPrefabs[index], spawnPos, Quaternion.identity);
    }
}