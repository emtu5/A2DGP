using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    public Transform firePoint;

    public AmmoData defaultAmmo;
    public AmmoLibrary ammoLibrary;
    public AmmoUI ammoUI;

    private IAmmoState currentState;
    private AmmoInventory inventory;

    private Queue<AmmoQueueItem> ammoQueue = new Queue<AmmoQueueItem>();

    private AmmoData currentAmmoData;
    private int currentAmmoLeft;

    void Start()
    {
        inventory = new AmmoInventory(0);

        currentState = new DefaultAmmoState();
        currentState.Enter(this);

        ammoUI.SetDefault();

        Debug.Log("PlayerAmmo STARTED");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) AddAmmoPickup(AmmoType.Fire, 5);
        if (Input.GetKeyDown(KeyCode.Alpha2)) AddAmmoPickup(AmmoType.Ice, 5);
        if (Input.GetKeyDown(KeyCode.Alpha3)) AddAmmoPickup(AmmoType.Poison, 5);
    }

    public void AddAmmoPickup(AmmoType type, int amount)
    {
        Debug.Log($"Pickup: {type} x{amount}");

        inventory.Add(type, amount);

        AmmoData data = ammoLibrary.GetAmmo(type);
        if (data == null)
        {
            Debug.LogError("AmmoData missing: " + type);
            return;
        }

        ammoQueue.Enqueue(new AmmoQueueItem(data, amount));

        ammoUI.UpdateQueue(ammoQueue);

        if (currentState is DefaultAmmoState)
            ActivateNextAmmo();
    }

    private void ActivateNextAmmo()
    {
        if (ammoQueue.Count == 0)
        {
            ChangeState(new DefaultAmmoState());
            ammoUI.SetDefault();
            return;
        }

        AmmoQueueItem next = ammoQueue.Dequeue();

        currentAmmoData = next.data;
        currentAmmoLeft = next.amount;

        ammoUI.UpdateQueue(ammoQueue);

        ChangeState(new LimitedAmmoState(next.data, next.amount));
        UpdateAmmoUI(next.data.ammoType, next.amount);
    }

    public void ChangeState(IAmmoState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void Shoot(Vector2 direction)
    {
        currentState.Shoot(this, direction);
    }

    public void OnAmmoFinished()
    {
        ActivateNextAmmo();
    }

    public void SpawnArrow(AmmoData data, Vector2 direction)
    {
        GameObject arrowObj = ArrowPool.Instance.GetArrow();
        arrowObj.transform.position = firePoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        Arrow arrow = arrowObj.GetComponent<Arrow>();
        arrow.Initialize(data, direction);
    }

    public void UpdateAmmoUI(AmmoType type, int amount)
    {
        AmmoData data = ammoLibrary.GetAmmo(type);

        if (ammoUI != null && data != null)
        {
            ammoUI.UpdateUI(data, amount);
        }
    }

    public void UpdateAmmoAfterShot()
    {
        currentAmmoLeft--;

        if (currentAmmoData != null)
        {
            UpdateAmmoUI(currentAmmoData.ammoType, currentAmmoLeft);
        }
    }

    public int GetAmmoCount(AmmoType type)
    {
        return inventory.Get(type);
    }

    public bool TryConsumeAmmo(AmmoType type)
    {
        return inventory.Consume(type);
    }
}