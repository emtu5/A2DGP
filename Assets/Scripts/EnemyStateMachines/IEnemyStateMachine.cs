using UnityEngine;

public interface IEnemyStateMachine
{
    Transform transform { get; }
    Animator animator { get; }
    BulletSpawner bulletSpawner { get; }

    void SetMovementEnabled(bool enabled);
    void SetAnimationBool(string name, bool value);
    void SetAnimationFloat(string name, float value);
    void SetAnimationTrigger(string name);
    void ChangeState(IEnemyState newState);
}