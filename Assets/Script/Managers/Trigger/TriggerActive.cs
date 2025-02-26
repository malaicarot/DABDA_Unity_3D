using UnityEngine;

public class TriggerActive : MonoBehaviour
{
    LoadScene loadScene;
    void Start()
    {
        loadScene = FindFirstObjectByType<LoadScene>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("LastScene"))
            {
                StartCoroutine(loadScene.WaitForLoad(3));
            }
            else
            {
                StartCoroutine(loadScene.WaitForLoad(1));
            }

            if (gameObject.CompareTag("Respawn"))
            {
                StartCoroutine(loadScene.WaitForLoad(0));
            }
        }
    }
}
