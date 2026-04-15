using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool pool;

    public void Release()
    {
        pool.PushToPool(this);
    }
}
