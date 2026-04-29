using UnityEngine;

[CreateAssetMenu(menuName = "Ammo/Ammo Library")]
public class AmmoLibrary : ScriptableObject
{
    public AmmoEntry[] entries;

    public AmmoData GetAmmo(AmmoType type)
    {
        foreach (var entry in entries)
        {
            if (entry.type == type)
                return entry.data;
        }

        Debug.LogWarning("Ammo not found: " + type);
        return null;
    }
}

[System.Serializable]
public class AmmoEntry
{
    public AmmoType type;
    public AmmoData data;
}