using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();

    private PooledMonoBehaviour prefab; //Why not cache this?

    private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();
    //Queue vs Linq.FirstOrDefault/OrderBy

    public static Pool GetPool(PooledMonoBehaviour prefab) //why does this need to be static?
    {
        if (pools.ContainsKey(prefab))
            return pools[prefab];

        var poolGameObject = new GameObject("Pool - " + prefab.name);
        var pool = poolGameObject.AddComponent<Pool>();

        pool.prefab = prefab;

        pools.Add(prefab, pool);
        return pool;
    }

    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (objects.Count == 0)
        {
            GrowPool();
        }

        var pooledObject = objects.Dequeue();
        return pooledObject as T;
    }

    private void GrowPool()
    {
        for (int i = 0; i < prefab.InitialPoolSize; i++)
        {
            var pooledObject = Instantiate(prefab) as PooledMonoBehaviour; //why cast this?
            pooledObject.gameObject.name += string.Empty + i;

            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
        }
    }

    private void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        pooledObject.transform.SetParent(this.transform);
        objects.Enqueue(pooledObject);
    }
}
