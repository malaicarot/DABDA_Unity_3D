using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStraight : MonoBehaviour
{
    [SerializeField] GameObject straight;
    [SerializeField] GameObject roundabound;
    [SerializeField] GameObject bounce;
    [SerializeField] Transform parent;
    [SerializeField] float x = 7;
    [SerializeField] float y = 2;
    [SerializeField] float z = 7;
    [SerializeField] float zDistanceBridge = 4;
    [SerializeField] int straightQuantity = 10;
    [SerializeField] int straightTurnQuantity = 3;
    [SerializeField] Vector3 originPosition;

    [SerializeField] int straightLine = 3;
    int line = 0;
    void Start()
    {
        originPosition = new Vector3(729, 555, -84);
        // straight.transform.SetParent(parent);
        SpawnRoad();
        SpawnBridge();
    }

    void SpawnRoad()
    {
        for (int i = 0; i < straightQuantity; i++)
        {
            if (line == straightLine)
            {
                for (int j = 0; j < straightTurnQuantity; j++)
                {

                    GameObject straightChild = Instantiate(straight, originPosition, Quaternion.identity);
                    straightChild.transform.SetParent(parent);
                    originPosition.z -= z;
                    originPosition.y += y;
                }
                line = 0;
            }
            GameObject turnChild = Instantiate(straight, originPosition, Quaternion.identity);
            turnChild.transform.SetParent(parent);

            originPosition.x -= x;
            originPosition.y += y;
            line++;
        }
    }

    void SpawnBridge()
    {
        for (int i = 0; i < straightQuantity; i++)
        {
            if (i > 0 && i % 2 == 0)
            {
                GameObject trap = Instantiate(roundabound, originPosition, Quaternion.Euler(0, 90, 0));
                trap.transform.SetParent(parent);
            }
            GameObject straightChild = Instantiate(straight, originPosition, Quaternion.identity);
            straightChild.transform.SetParent(parent);

            originPosition.z -= zDistanceBridge;
            if (i + 1 == straightQuantity)
            {
                GameObject bounceChild = Instantiate(bounce, originPosition, Quaternion.Euler(90, 0, 0));
                bounceChild.transform.SetParent(parent);

            }
        }
    }
}
