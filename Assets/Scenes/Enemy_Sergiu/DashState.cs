using UnityEngine;

public class DashState : IEnemyState
{
    private float speed;
    private Transform player;
    private Vector2 direction;

    public DashState(float speed, Transform player)
    {
        this.speed = speed;
        this.player = player;
    }

    public void Enter(IEnemyStateMachine enemy)
    {
        if (player != null)
            direction = (player.position - enemy.transform.position).normalized;
        else
            direction = Random.insideUnitCircle.normalized;

        enemy.SetMovementEnabled(true);
        enemy.SetAnimationBool("IsMoving", true);
        enemy.SetAnimationTrigger("Dash");   // optional – create this trigger in your animator
    }

    public void Update(IEnemyStateMachine enemy)
    {
        enemy.transform.Translate(direction * speed * Time.deltaTime);
        enemy.SetAnimationFloat("MoveX", direction.x);
        enemy.SetAnimationFloat("MoveY", direction.y);
    }

    public void Exit(IEnemyStateMachine enemy)
    {
        enemy.SetMovementEnabled(false);
    }
}