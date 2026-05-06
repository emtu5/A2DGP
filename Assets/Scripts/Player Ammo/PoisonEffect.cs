using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Effects/Poison")]
public class PoisonEffect : AmmoEffect
{
    public float damagePerTick = 2f;
    public float duration = 5f;
    public float tickRate = 1f;

    public override void Apply(GameObject target, float damage)
    {
        EnemyEffects effects = target.GetComponent<EnemyEffects>();

        if (effects != null)
        {
            effects.ApplyPoison(damagePerTick, duration, tickRate);
        }
    }
}