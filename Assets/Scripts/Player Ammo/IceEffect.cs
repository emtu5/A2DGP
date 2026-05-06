using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Effects/Ice")]
public class IceEffect : AmmoEffect
{
    public float slowMultiplier = 0.5f;
    public float duration = 2f;

    public override void Apply(GameObject target, float damage)
    {
        EnemyEffects effects = target.GetComponent<EnemyEffects>();

        if (effects != null)
        {
            effects.ApplySlow(slowMultiplier, duration);
        }
    }
}