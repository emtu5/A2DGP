using UnityEngine;

public abstract class AmmoEffect : ScriptableObject
{
    public abstract void Apply(GameObject target, float damage);
}