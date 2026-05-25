using UnityEngine;

public class ShootingState : IEnemyState
{
    private float shootTimer;
    private float timeInState;
    private float stateDuration = 3f;
    private float firingSpeedMultiplier = 1f;
    private bool autoTransition;

    // Constructor with optional autoTransition flag (default true for Boss1)
    public ShootingState(bool autoTransition = true)
    {
        this.autoTransition = autoTransition;
    }

    public void Enter(IEnemyStateMachine enemy)
    {
        shootTimer = 0f;
        timeInState = 0f;
        enemy.SetMovementEnabled(false);
        enemy.SetAnimationBool("IsMoving", false);
        enemy.SetAnimationBool("IsAttacking", true);
        Debug.Log("Entered Shooting State");
    }

    public void Update(IEnemyStateMachine enemy)
    {
        timeInState += Time.deltaTime;

        if (autoTransition && timeInState >= stateDuration)
        {
            enemy.ChangeState(new MovingState(true));   // auto‑transition back to Moving for Boss1
            return;
        }

        shootTimer += Time.deltaTime;
        if (shootTimer >= enemy.bulletSpawner.Pattern.firingRate * firingSpeedMultiplier)
        {
            enemy.bulletSpawner.FireBullets();
            shootTimer = 0f;
            enemy.SetAnimationTrigger("Shoot");
        }
    }

    public void Exit(IEnemyStateMachine enemy)
    {
        Debug.Log("Exited Shooting State");
    }

    public void SetFiringSpeedMultiplier(float multiplier) => firingSpeedMultiplier = multiplier;
}