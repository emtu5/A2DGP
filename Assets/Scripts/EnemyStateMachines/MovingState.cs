using UnityEngine;

public class MovingState : IEnemyState
{
    private Vector2 movementDirection;
    private float changeDirectionTimer;
    private float moveTimer;
    private float changeDirectionTime = 2f;
    private float moveDuration = 3f;
    private float moveSpeed = 2f;
    private float speedMultiplier = 1f;
    private bool autoTransition;

    // Constructor with optional autoTransition flag (default true for Boss1)
    public MovingState(bool autoTransition = true)
    {
        this.autoTransition = autoTransition;
    }

    public void Enter(IEnemyStateMachine enemy)
    {
        ChooseNewDirection();
        changeDirectionTimer = changeDirectionTime;
        moveTimer = 0f;
        enemy.SetMovementEnabled(true);
        enemy.SetAnimationBool("IsMoving", true);
        enemy.SetAnimationBool("IsAttacking", false);
        Debug.Log("Entered Moving State");
    }

    public void Update(IEnemyStateMachine enemy)
    {
        moveTimer += Time.deltaTime;

        // Only auto‑transition if flag is true
        if (autoTransition && moveTimer >= moveDuration)
        {
            enemy.ChangeState(new ShootingState(true));   // auto‑transition to Shooting for Boss1
            return;
        }

        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChooseNewDirection();
            changeDirectionTimer = changeDirectionTime;
        }

        enemy.transform.Translate(movementDirection * moveSpeed * speedMultiplier * Time.deltaTime);
        enemy.SetAnimationFloat("MoveX", movementDirection.x);
        enemy.SetAnimationFloat("MoveY", movementDirection.y);
    }

    public void Exit(IEnemyStateMachine enemy)
    {
        enemy.SetMovementEnabled(false);
        Debug.Log("Exited Moving State");
    }

    private void ChooseNewDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized;
    }

    public void SetSpeedMultiplier(float multiplier) => speedMultiplier = multiplier;
}