using System.Collections;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    private Coroutine slowRoutine;
    private Coroutine poisonRoutine;
    private Coroutine flashRed;

    private IEnemyStateMachine stateMachine;
    private HealthSystem health;
    private SpriteRenderer enemySprite;

    void Awake()
    {
        stateMachine = GetComponent(typeof(IEnemyStateMachine)) as IEnemyStateMachine;
        health = GetComponent<HealthSystem>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (slowRoutine != null)
            StopCoroutine(slowRoutine);

        slowRoutine = StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        if (stateMachine != null)
        {
            stateMachine.SetSpeedMultiplier(multiplier);
            stateMachine.SetFiringSpeedMultiplier(1f / multiplier);
        }
            
        yield return new WaitForSeconds(duration);

        if (stateMachine != null)
        {
            stateMachine.SetSpeedMultiplier(1f);
            stateMachine.SetFiringSpeedMultiplier(1f);
        }
    }

    public void ApplyPoison(float damagePerTick, float duration, float tickRate)
    {
        if (poisonRoutine != null)
            StopCoroutine(poisonRoutine);

        poisonRoutine = StartCoroutine(PoisonRoutine(damagePerTick, duration, tickRate));
    }

    private IEnumerator PoisonRoutine(float damagePerTick, float duration, float tickRate)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (health == null)
                yield break;

            health.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(tickRate);
            elapsed += tickRate;
        }
    }

    public void FlashRed()
    {
        Camera.main.GetComponent<CameraShake>().Shake(0.15f, 0.08f);
        if (enemySprite != null)
        {
            if (flashRed != null)
            {
                StopCoroutine(flashRed);
            }
            flashRed = StartCoroutine(FlashRoutine(enemySprite));
        }
    }

    private IEnumerator FlashRoutine(SpriteRenderer sprite)
    {
        Color originalColor = sprite.color;
        sprite.color = new Color(1f, 0.5f, 0.5f, originalColor.a);
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        flashRed = null;
    }
}