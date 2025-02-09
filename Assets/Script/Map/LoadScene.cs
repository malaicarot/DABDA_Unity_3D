using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    

    public void LoadNextScene(){
        StartCoroutine(WaitForLoad(1));
    }
    public void LoadCurrentScene(){
        StartCoroutine(WaitForLoad(0));
    }

    public void LoadLastScene(){
        StartCoroutine(WaitForLoad(2));
    }
    void LoadSceneManager(int nextIndex)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + nextIndex);        
    }
    // IEnumerator WaitLoad(){
    //     SceneManager.LoadScene(4);
    // }

    IEnumerator WaitForLoad(int nextIndex){
        yield return new WaitForSeconds(2);
        LoadSceneManager(nextIndex);
    }
}
