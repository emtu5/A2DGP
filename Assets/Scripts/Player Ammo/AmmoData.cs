using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Ammo Data")]
public class AmmoData : ScriptableObject
{
    public AmmoType ammoType;
    public float damage;
    public float speed;
    public AmmoEffect effect;
}