using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    LoadScene loadScene;
    void Start(){
        loadScene = FindFirstObjectByType<LoadScene>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            loadScene.LoadCurrentScene();
            Debug.Log("Die");
        }
    }
}
