using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Effects/Ice")]
public class IceEffect : AmmoEffect
{
    public override void Apply(GameObject target, float damage)
    {
        Debug.Log("Slowing target");
    }
}