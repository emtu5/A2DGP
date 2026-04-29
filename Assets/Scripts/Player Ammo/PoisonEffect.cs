using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Effects/Fire")]
public class PoisonEffect : AmmoEffect
{
    public override void Apply(GameObject target, float damage)
    {
        Debug.Log("Damage over time target for " + damage);
    }
}