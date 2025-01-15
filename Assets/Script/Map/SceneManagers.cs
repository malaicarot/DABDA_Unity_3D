using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
