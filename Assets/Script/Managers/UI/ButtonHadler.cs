using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHadler : MonoBehaviour
{
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject settingsPannel;
    PlayerBeforeStart playerBeforeStart;
    UIManagers uIManagers;
    [SerializeField] PlayerInventory playerInventory;

    private void Start()
    {
        playerBeforeStart = FindFirstObjectByType<PlayerBeforeStart>();
        uIManagers = FindFirstObjectByType<UIManagers>();
    }

    public void StartGame()
    {
        // if (SaveManager.SingletonSaveData.saveData.inventoryDatas.Count > 0)
        // {
            SaveManager.SingletonSaveData.DeleteFileSave();
        // }
        // playerInventory.LoadGameData();
        // StartCoroutine(WaitForLoadData());
        SetFalseUI();
    }
    IEnumerator WaitForLoadData(){
        yield return new WaitForSeconds(2);
    }

    private void SetFalseUI()
    {
        playerBeforeStart.EnableFeature(true);
        playerBeforeStart.CursorHandle(true);
        playerBeforeStart.setCameraRoot(true);
        startUI.SetActive(false);
        uIManagers.ActiveInventory(true);
    }

    public void SettingsGame()
    {
        settingsPannel.SetActive(true);
        
    }

    public void LoadCheckPoint()
    {
        playerInventory.LoadGameData();
        if (SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0)
        {
            int index = SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count - 1;
            if (index != 0)
            {
                int map = SaveManager.SingletonSaveData.checkPointData.checkpointDatas[index].mapIndex;
                SceneManager.LoadScene(map);
            }
            SetFalseUI();
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
