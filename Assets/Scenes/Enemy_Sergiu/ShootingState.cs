using UnityEngine;

public class ShootingState : IEnemyState
{
    private float shootTimer;
    private float timeInState;

    private float stateDuration = 3f;

    public void Enter(EnemyStateMachine enemy)
    {
        shootTimer = 0f;
        timeInState = 0f;
        enemy.animator.SetBool("IsMoving", false);
        enemy.animator.SetBool("IsAttacking", true);
        Debug.Log("Entered Shooting State");
    }

    public void Update(EnemyStateMachine enemy)
    {
        // Count total time in shooting state
        timeInState += Time.deltaTime;

        // Leave shooting state after some seconds
        if (timeInState >= stateDuration)
        {
            enemy.ChangeState(new TeleportState());
            return;
        }

        // Fire bullets based on pattern fire rate
        shootTimer += Time.deltaTime;

        if (shootTimer >= enemy.bulletSpawner.Pattern.firingRate)
        {
            enemy.bulletSpawner.FireBullets();
            shootTimer = 0f;
        }
    }

    public void Exit(EnemyStateMachine enemy)
    {
        Debug.Log("Exited Shooting State");
    }
}
