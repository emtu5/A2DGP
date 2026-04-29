using System;

[Serializable]
public class AmmoQueueItem
{
    public AmmoData data;
    public int amount;

    public AmmoQueueItem(AmmoData data, int amount)
    {
        this.data = data;
        this.amount = amount;
    }
}