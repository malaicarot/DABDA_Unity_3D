using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] uint poolSize = 10;
    [SerializeField] List<PooledObject> objectToPool;
    Dictionary<string, Stack<PooledObject>> poolDictionary;


    void Start()
    {
        SetUpPool();
    }

    void SetUpPool()
    {
        if (objectToPool == null || objectToPool.Count == 0)
        {
            return;
        }
        poolDictionary = new Dictionary<string, Stack<PooledObject>>();
        foreach (var obj in objectToPool)
        {
            Stack<PooledObject> objStack = new Stack<PooledObject>();
            for (int i = 0; i < poolSize; i++)
            {
                PooledObject instance = Instantiate(obj);
                instance.Pool = this;
                instance.gameObject.name = obj.name;
                instance.gameObject.SetActive(false);
                objStack.Push(instance);
            }
            poolDictionary.Add(obj.name, objStack);
        }

    }
    public PooledObject GetPooledObject(string objType)
    {
        if (string.IsNullOrEmpty(objType) || !poolDictionary.ContainsKey(objType))
        {
            return null;
        }
        if (poolDictionary[objType].Count == 0)
        {
            PooledObject newInstance = Instantiate(objectToPool.Find(obj => obj.name == objType));
            newInstance.Pool = this;
            newInstance.gameObject.name = objType;

            return newInstance;
        }

        PooledObject currentInstance = poolDictionary[objType].Pop();
        currentInstance.gameObject.SetActive(true);

        return currentInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        if (!poolDictionary.ContainsKey(pooledObject.name))
        {
            Destroy(pooledObject.gameObject);
        }
        else
        {
            poolDictionary[pooledObject.name].Push(pooledObject);
            pooledObject.gameObject.SetActive(false);
        }
    }
}
