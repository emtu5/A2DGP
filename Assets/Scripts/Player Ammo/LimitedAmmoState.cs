using UnityEngine;

public class LimitedAmmoState : IAmmoState
{
    private AmmoData ammoData;
    private int currentAmmo;

    public LimitedAmmoState(AmmoData data)
    {
        ammoData = data;
        currentAmmo = data.ammoAmount;
    }

    public void Enter(PlayerAmmo player)
    {
        Debug.Log("Entered: " + ammoData.ammoType + " | Ammo: " + currentAmmo);
    }

    public void Exit(PlayerAmmo player) { }

    public void Shoot(PlayerAmmo player, Vector2 direction)
    {
        if (currentAmmo <= 0)
        {
            player.ChangeState(new DefaultAmmoState());
            return;
        }

        player.SpawnArrow(ammoData, direction);

        currentAmmo--;

        Debug.Log(ammoData.ammoType + " left: " + currentAmmo);

        if (currentAmmo <= 0)
        {
            player.ChangeState(new DefaultAmmoState());
        }
    }
}