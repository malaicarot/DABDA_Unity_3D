using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHadler : MonoBehaviour
{
    [SerializeField] GameObject StartUI;

    PlayerBeforeStart playerBeforeStart;

    private void Start()
    {
        playerBeforeStart = FindFirstObjectByType<PlayerBeforeStart>();
    }


    public void StartGame()
    {
        playerBeforeStart.EnableFeature(true);
        playerBeforeStart.CursorHandle(true);
        playerBeforeStart.setCameraRoot(true);
        StartUI.SetActive(false);

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
