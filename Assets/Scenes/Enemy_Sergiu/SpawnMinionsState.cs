using UnityEngine;

public class SpawnMinionsState : IEnemyState
{
    private GameObject minionPrefab;
    private int count;
    private float radius;

    public SpawnMinionsState(GameObject prefab, int count, float radius)
    {
        this.minionPrefab = prefab;
        this.count = count;
        this.radius = radius;
    }

    public void Enter(IEnemyStateMachine enemy)
    {
        enemy.SetMovementEnabled(false);
        enemy.SetAnimationBool("IsMoving", false);
        enemy.SetAnimationBool("IsAttacking", false);
        enemy.SetAnimationTrigger("Spawn");   
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * radius;
            Vector3 spawnPos = enemy.transform.position + (Vector3)offset;
            Object.Instantiate(minionPrefab, spawnPos, Quaternion.identity);
        }
        Debug.Log($"Spawned {count} minions");
    }

    public void Update(IEnemyStateMachine enemy) { }   
    public void Exit(IEnemyStateMachine enemy) { }
}