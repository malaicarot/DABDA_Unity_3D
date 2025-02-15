using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject coutinueButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject exitButton;

    void Start()
    {
        if (!(SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0))
        {
            coutinueButton.SetActive(false);
        }else{
            coutinueButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
