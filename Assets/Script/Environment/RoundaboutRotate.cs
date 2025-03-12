using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundaboutRotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 8f;

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }
}
