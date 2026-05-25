using UnityEngine;

[CreateAssetMenu(fileName = "AttackPattern", menuName = "Scriptable Objects/AttackPattern")]
public class AttackPattern : ScriptableObject
{
    public float moveSpeed = 6f;
    public float acceleration = 0f;
    public float startingAngle = 0f;
    public float angleStep = 0f;
    public float firingRate = 1f;
    public float numberOfBullets = 6f;
    public float lifetime = 1f;
    public float homingTimer = 0f;
    public bool isFadeOut = false;
    public Sprite bulletSprite;
}
