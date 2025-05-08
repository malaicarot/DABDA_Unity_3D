using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    LoadScene loadScene;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(canvas);
        loadScene = FindFirstObjectByType<LoadScene>();
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         loadScene.WaitForLoad(1);
    //     }

    // }
}
