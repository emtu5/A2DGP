using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private int initCount;
    [SerializeField]
    private PooledObject objTemplate;
    private Stack<PooledObject> objectPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPool = new Stack<PooledObject>();
        for (int i = 0; i < initCount; i++)
        {
            PooledObject newObj = Instantiate(objTemplate, transform);
            newObj.pool = this;
            newObj.gameObject.SetActive(false);
            objectPool.Push(newObj);
        }
    }

    public PooledObject GetPooledObject()
    {
        if (objectPool.Count == 0)
        {
            PooledObject newObj = Instantiate(objTemplate, transform);
            newObj.pool = this;
            // newObj.gameObject.SetActive(false);
            return newObj;
        }

        PooledObject nextObj = objectPool.Pop();
        nextObj.gameObject.SetActive(true);
        return nextObj;
    }

    public void PushToPool(PooledObject obj)
    {
        objectPool.Push(obj);
        obj.gameObject.SetActive(false);
    }
}
