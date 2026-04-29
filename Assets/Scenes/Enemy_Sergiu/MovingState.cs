using UnityEngine;

public class MovingState : IEnemyState
{
    private Vector2 movementDirection;
    private float changeDirectionTimer;
    private float moveTimer;

    private float changeDirectionTime = 2f;
    private float moveDuration = 3f;   // how long enemy moves before shooting
    private float moveSpeed = 2f;

    public void Enter(EnemyStateMachine enemy)
    {
        ChooseNewDirection();
        changeDirectionTimer = changeDirectionTime;
        moveTimer = 0f;
        Debug.Log("Entered Moving State");
    }

    public void Update(EnemyStateMachine enemy)
    {
        // COUNT total time in moving state
        moveTimer += Time.deltaTime;

        // After some seconds → switch to shooting
        if (moveTimer >= moveDuration)
        {
            enemy.ChangeState(new ShootingState());
            return;
        }

        // Change direction occasionally
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChooseNewDirection();
        }

        // Move enemy
        enemy.transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }

    public void Exit(EnemyStateMachine enemy)
    {
        Debug.Log("Exited Moving State");
    }

    private void ChooseNewDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized;
        changeDirectionTimer = changeDirectionTime;
    }
}
