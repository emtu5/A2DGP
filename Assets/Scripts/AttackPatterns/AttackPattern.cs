using UnityEngine;

[CreateAssetMenu(fileName = "AttackPattern", menuName = "Scriptable Objects/AttackPattern")]
public class AttackPattern : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float acceleration = 0f;
    public float startingAngle = 0f;
    public float angleStep = 0f;
    [Header("Firing")]
    public float firingRate = 1f;
    public float numberOfBullets = 6f;
    [Header("Timers")]
    public float lifetime = 1f;
    public float homingTimer = 0f;
    [Header("Bullet")]
    public bool isFadeOut = false;
    public Sprite bulletSprite;
    public float hitCircleRadius = 0.5f;
}
