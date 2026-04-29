using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AmmoInventory
{
    private Dictionary<AmmoType, int> ammo = new Dictionary<AmmoType, int>();

    public AmmoInventory(int startingAmount)
    {
        foreach (AmmoType type in Enum.GetValues(typeof(AmmoType)))
        {
            ammo[type] = startingAmount;
        }
    }

    public int Get(AmmoType type)
    {
        return ammo[type];
    }

    public void Add(AmmoType type, int amount)
    {
        ammo[type] += amount;
    }

    public bool Consume(AmmoType type)
    {
        if (ammo[type] <= 0)
            return false;

        ammo[type]--;
        return true;
    }
}