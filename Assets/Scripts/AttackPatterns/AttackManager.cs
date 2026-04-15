using System.Threading;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private BulletSpawner bulletSpawner;
    private AttackBank attackBank;
    private bool timerRunning = true;
    [SerializeField]
    private float swapFrequency = 10f;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
        attackBank = GetComponent<AttackBank>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= swapFrequency)
        {
            bulletSpawner.SetAttackPattern(attackBank.GetRandomPattern());
            timer = 0;
        }
    }
}
