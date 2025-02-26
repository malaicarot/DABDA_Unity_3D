using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadMap : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitToLoadMenuGame());
    }
    
    IEnumerator WaitToLoadMenuGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
