using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainActive : MonoBehaviour
{
    ParticleSystem[] rain;
    void Start()
    {
        rain = GetComponentsInChildren<ParticleSystem>();
    }

    public void Active()
    {
        foreach (ParticleSystem item in rain)
        {
            item.Play();
        }
    }
}
