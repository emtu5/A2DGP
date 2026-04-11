using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Ammo Data")]
public class AmmoData : ScriptableObject
{
    public AmmoType ammoType;

    [Header("Stats")]
    public float damage = 10f;
    public float speed = 10f;

    [Header("Limited Ammo")]
    public int ammoAmount = 5;

    [Header("Effects")]
    public bool slowEffect;
    public bool poisonEffect;
}