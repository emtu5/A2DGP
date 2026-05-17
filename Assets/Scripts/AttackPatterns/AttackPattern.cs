using UnityEngine;

[CreateAssetMenu(fileName = "AttackPattern", menuName = "Scriptable Objects/AttackPattern")]
public class AttackPattern : ScriptableObject
{
    [SerializeField]
    public float moveSpeed = 6f;
    [SerializeField]
    public float acceleration = 0f;
    [SerializeField]
    public float startingAngle = 0f;
    [SerializeField]
    public float angleStep = 0f;
    [SerializeField]
    public float firingRate = 1f;
    [SerializeField]
    public float numberOfBullets = 6f;
    [SerializeField]
    public float lifetime = 1f;
    [SerializeField]
    public float homingTimer = 0f;
}
