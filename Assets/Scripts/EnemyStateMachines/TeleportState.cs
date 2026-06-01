using UnityEngine;
using System.Collections;

public class TeleportState : IEnemyState
{
    private float delay;
    private float minX, maxX, minY, maxY;
    private Color flashColor;
    private float flashDuration;

    public TeleportState(float delay, float minX, float maxX, float minY, float maxY, 
                         Color flashColor, float flashDuration)
    {
        this.delay = delay;
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
        this.flashColor = flashColor;
        this.flashDuration = flashDuration;
    }

    private float timer;

    public void Enter(IEnemyStateMachine enemy)
    {
        timer = 0f;
        enemy.SetMovementEnabled(false);
        enemy.SetAnimationBool("IsMoving", false);
        enemy.SetAnimationBool("IsAttacking", false);
        enemy.SetAnimationTrigger("Teleport");
    }

    public void Update(IEnemyStateMachine enemy)
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            // Pick random position within bounds
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector2 newPos = new Vector2(x, y);
            enemy.transform.position = newPos;

            // Optional: play a flash effect if the enemy has SpriteRenderer and machine has a helper
            if (enemy is FinalBossStateMachine finalBoss)
            {
                finalBoss.StartCoroutine(finalBoss.DoTeleportFlash());
            }

            // Teleport done – the state machine will change state after its duration ends.
            // We don't auto-change here; the machine timer will pick the next state.
            // But we reset the timer so we don't teleport multiple times in one state.
            timer = -1f; // prevent multiple teleports
        }
    }

    public void Exit(IEnemyStateMachine enemy) { }
}