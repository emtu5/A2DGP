using UnityEngine;

public interface IAmmoState
{
    void Enter(PlayerAmmo player);
    void Exit(PlayerAmmo player);
    void Shoot(PlayerAmmo player, Vector2 direction);
}