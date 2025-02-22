using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{

    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject coutinueButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject exitButton;
    /*******************************************/
    [SerializeField] List<GameObject> inventoryUI;

    void Start()
    {

        ActiveInventory(false);
        if (!(SaveManager.SingletonSaveData.checkPointData.checkpointDatas.Count > 0))
        {
            coutinueButton.SetActive(false);
        }
        else
        {
            coutinueButton.SetActive(true);
        }
    }

    public void ActiveInventory(bool active)
    {
        foreach (GameObject item in inventoryUI)
        {
            item.SetActive(active);
        }
    }
}
