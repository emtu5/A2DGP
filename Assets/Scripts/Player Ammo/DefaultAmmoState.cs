using UnityEngine;

public class DefaultAmmoState : IAmmoState
{
    public void Enter(PlayerAmmo player)
    {
        Debug.Log("Default Ammo Active (Infinite)");
    }

    public void Exit(PlayerAmmo player) { }

    public void Shoot(PlayerAmmo player, Vector2 direction)
    {
        player.SpawnArrow(player.defaultAmmo, direction);
    }
}