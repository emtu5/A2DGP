using UnityEngine;

public class LimitedAmmoState : IAmmoState
{
    private AmmoData ammoData;
    private int ammoLeft;

    public LimitedAmmoState(AmmoData data, int amount)
    {
        ammoData = data;
        ammoLeft = amount;
    }

    public void Enter(PlayerAmmo player)
    {
        player.UpdateAmmoUI(ammoData.ammoType, ammoLeft);
    }

    public void Exit(PlayerAmmo player) { }

    public void Shoot(PlayerAmmo player, Vector2 direction)
    {
        if (ammoLeft <= 0)
        {
            player.OnAmmoFinished();
            return;
        }

        if (!player.TryConsumeAmmo(ammoData.ammoType))
        {
            player.OnAmmoFinished();
            return;
        }

        ammoLeft--;

        player.SpawnArrow(ammoData, direction);

        player.UpdateAmmoUI(ammoData.ammoType, ammoLeft);

        if (ammoLeft <= 0)
        {
            player.OnAmmoFinished();
        }
    }
}