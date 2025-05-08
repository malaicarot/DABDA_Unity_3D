using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
    void Start()
    {
        SaveManager.SingletonSaveData.LoadCombinedData();
    }
    public void LoadSaveScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCurrentScene()
    {
        StartCoroutine(WaitForLoad(0));
    }
    public void LoadFinalScene()
    {
        StartCoroutine(WaitForLoad(2));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadSceneManager(int nextIndex)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + nextIndex);
    }

    public void LoadEndScene()
    {
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastSceneIndex);
    }
   

    public IEnumerator WaitForLoad(int nextIndex)
    {
        yield return new WaitForSeconds(2);
        LoadSceneManager(nextIndex);
    }
}
