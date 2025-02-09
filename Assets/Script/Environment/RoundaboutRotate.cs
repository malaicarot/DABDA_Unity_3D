using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundaboutRotate : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(8.0f, 0.0f, 0.0f, Space.Self);
    }
}
