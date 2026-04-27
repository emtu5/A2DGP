using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool Instance;

    [Header("Pool Settings")]
    public GameObject arrowPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(arrowPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetArrow()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(arrowPrefab, transform);
            return obj;
        }
    }

    public void ReturnArrow(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}