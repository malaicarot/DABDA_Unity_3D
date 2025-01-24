using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    LoadScene loadScene;
    void Start()
    {
        loadScene = FindFirstObjectByType<LoadScene>();

        // DontDestroyOnLoad(gameObject);
        // DontDestroyOnLoad(canvas);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            loadScene.loadScene();
        }

    }
}
