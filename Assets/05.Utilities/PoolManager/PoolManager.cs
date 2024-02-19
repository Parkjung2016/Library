using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance;

    private Dictionary<string, Pool<MonoBehaviour>> _pools
        = new();

    private Transform _trmParent;

    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }

    public void CreatePool(GameObject prefab, int count = 10)
    {
        Pool<MonoBehaviour> pool =
            new Pool<MonoBehaviour>(prefab.GetComponent<MonoBehaviour>(), _trmParent, count);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public PoolableMono Pop(string name)
    {
        if (!_pools.ContainsKey(name))
        {
            Debug.LogError($"Prefab does not exist on pool : {name}");
            return null;
        }

        MonoBehaviour obj = _pools[name].Pop();
        PoolableMono item = obj.GetComponent<PoolableMono>();
        item.Init();
        return item;
    }

    public void Push(MonoBehaviour obj)
    {
        Debug.Log(obj.gameObject.name);
        _pools[obj.gameObject.name].Push(obj.GetComponent<MonoBehaviour>());
    }
}