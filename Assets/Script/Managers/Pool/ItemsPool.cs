using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPool : ObjectPool
{
    public static ItemsPool SingleTonItemsPool;

    void Awake()
    {
        SingleTonItemsPool = this;
    }

    public MarkPool GetItem(string name, Vector3 position, Quaternion quaternion){
        PooledObject objOfPool = SingleTonItemsPool.GetPooledObject(name);
        MarkPool item = objOfPool.GetComponent<MarkPool>();
        item.transform.position = position;
        item.transform.rotation = quaternion;
        return item;
    }
}
