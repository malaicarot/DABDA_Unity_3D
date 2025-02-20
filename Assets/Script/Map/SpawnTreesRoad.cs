using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreesRoad : MonoBehaviour
{
    [SerializeField] GameObject tree;
    [SerializeField] float xPos;
    [SerializeField] float yPos;
    [SerializeField] float zPos;
    [SerializeField] int quantity;
    List<GameObject> treeList = new List<GameObject>();

    public void SpawnTrees()
    {
        StartCoroutine(WaitGrowth());
    }
    IEnumerator WaitGrowth()
    {
        yield return new WaitForSeconds(2);
        Vector3 treePos = tree.transform.position;
        for (int i = 0; i <= quantity; i++)
        {
            GameObject treeRoad = Instantiate(tree, treePos, Quaternion.identity);
            treeList.Add(treeRoad);
            treeRoad.transform.SetParent(gameObject.transform);
            treePos.z += zPos;
            treePos.y += yPos;
        }
    }
}
