using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadMap : MonoBehaviour
{
    [SerializeField] GameObject startUI;
    void Start()
    {
       if (!(SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0))
        {
            startUI.SetActive(false);
            SceneManager.LoadScene(1);
        }else{
            startUI.SetActive(true);
        }
    }

    public void LoadCheckPoint()
    {
        if (SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0)
        {
            int index = SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1;
            int map = SaveManager.SingletonSaveData.checkPointData.checkpointDatas[index].mapIndex;
            SceneManager.LoadScene(map);
        }
    }

    


}
