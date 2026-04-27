using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    public AmmoData defaultAmmo;
    public AmmoData fireAmmo;
    public AmmoData iceAmmo;
    public AmmoData poisonAmmo;


    private IAmmoState currentState;

    void Start()
    {
        currentState = new DefaultAmmoState();
        currentState.Enter(this);
    }

    void Update()
    {
        // TEST SWITCHES
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(new LimitedAmmoState(defaultAmmo));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(new LimitedAmmoState(fireAmmo));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(new LimitedAmmoState(iceAmmo));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeState(new LimitedAmmoState(poisonAmmo));
        }
    }

    public void Shoot(Vector2 direction)
    {
        currentState.Shoot(this, direction);
    }

    public void ChangeState(IAmmoState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
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
}