
using UnityEngine;
[RequireComponent(typeof(PooledObject))]

public class MarkPool : PooledObject
{

    public void ItemRelease(){
        Release();
    }

}
