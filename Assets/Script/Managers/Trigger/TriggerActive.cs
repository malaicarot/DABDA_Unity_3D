using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActive : MonoBehaviour
{
    LoadScene loadScene;
    void Start()
    {
        loadScene = FindFirstObjectByType<LoadScene>();
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            loadScene.loadScene();
        }
    }

    
}
