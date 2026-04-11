using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    private IAmmoState currentState;

    void Start()
    {
        currentState = new DefaultAmmoState();
        currentState.Enter(this);
    }

    public void Shoot(Vector2 direction)
    {
        currentState.Shoot(this, direction);
    }

    public void SpawnArrow(Vector2 direction)
    {
        GameObject arrowObj = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        Arrow arrow = arrowObj.GetComponent<Arrow>();
        arrow.Initialize(direction);
    }

    public void ChangeState(IAmmoState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
}