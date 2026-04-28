using UnityEngine;

public class TeleportState : IEnemyState
{
    private float teleportDelay = 0.5f;   // Wait before teleport (visual cue)
    private float teleportDuration = 0.2f; // Teleport itself is instant, but we add a flash
    private float timer;

    public void Enter(EnemyStateMachine enemy)
    {
        timer = 0f;
        enemy.SetMovementEnabled(false);
        Debug.Log("Entered Teleport State - Preparing to teleport");
    }

    public void Update(EnemyStateMachine enemy)
    {
        timer += Time.deltaTime;

        if (timer >= teleportDelay)
        {
            Vector2 newPosition = enemy.GetRandomTeleportPosition();
            enemy.transform.position = newPosition;
            Debug.Log($"Teleported to {newPosition}");
            enemy.StartCoroutine(enemy.TeleportFlash());
            enemy.ChangeState(new MovingState());
        }
    }

    public void Exit(EnemyStateMachine enemy)
    {
        Debug.Log("Exited Teleport State");
    }
}
