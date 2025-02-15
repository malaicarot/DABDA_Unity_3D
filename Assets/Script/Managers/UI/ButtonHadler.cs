using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHadler : MonoBehaviour
{
    [SerializeField] GameObject startUI;

    PlayerBeforeStart playerBeforeStart;

    private void Start()
    {
        playerBeforeStart = FindFirstObjectByType<PlayerBeforeStart>();

        // if (!(SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0))
        // {
        //     startUI.SetActive(true);
        // }
        // else
        // {
        //     startUI.SetActive(false);
        // }
    }


    public void StartGame()
    {
        if (SaveManager.SingletonSaveData.saveData.inventoryDatas.Count > 0)
        {
            SaveManager.SingletonSaveData.DeleteFileSave();
        }

        playerBeforeStart.EnableFeature(true);
        playerBeforeStart.CursorHandle(true);
        playerBeforeStart.setCameraRoot(true);
        startUI.SetActive(false);

    }

    public void SettingsGame()
    {
        Debug.Log("Settings");

    }

    public void LoadCheckPoint()
    {
        if (SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0)
        {
            int index = SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1;
            int map = SaveManager.SingletonSaveData.checkPointData.checkpointDatas[index].mapIndex;
            SceneManager.LoadScene(map);
            playerBeforeStart.StartGame();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }
}
