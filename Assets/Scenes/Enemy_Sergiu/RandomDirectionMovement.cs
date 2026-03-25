using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2f;
    public float changeDirectionTime = 2f;

    private Vector2 movementDirection;
    private float timer;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ChooseNewDirection();
        }

        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void ChooseNewDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }
}
