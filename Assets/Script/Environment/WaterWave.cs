using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{
    [SerializeField] float xWave;
    [SerializeField] float yWave;

    Renderer waterRenderer;


    void Start()
    {
        waterRenderer = GetComponent<Renderer>();

    }

    void Update()
    {
        float offsetX = Time.time * xWave;
        float offsetY = Time.time * yWave;
        waterRenderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);

    }
}
