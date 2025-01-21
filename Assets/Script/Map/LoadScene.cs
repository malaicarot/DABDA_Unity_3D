using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{


    public void loadScene(){
        StartCoroutine(WaitForLoad());
    }
    void LoadSceneManager()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);        
    }

    IEnumerator WaitForLoad(){
        yield return new WaitForSeconds(2);
        LoadSceneManager();
    }
}
