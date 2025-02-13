using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHadler : MonoBehaviour
{
    [SerializeField] GameObject startUI;

    PlayerBeforeStart playerBeforeStart;

    private void Start()
    {
        playerBeforeStart = FindFirstObjectByType<PlayerBeforeStart>();

        if (!(SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0))
        {
            startUI.SetActive(true);
        }
        else
        {
            startUI.SetActive(false);
        }
    }


    public void StartGame()
    {
        playerBeforeStart.EnableFeature(true);
        playerBeforeStart.CursorHandle(true);
        playerBeforeStart.setCameraRoot(true);
        startUI.SetActive(false);

    }

    public void SettingsGame()
    {
        Debug.Log("Settings");

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
