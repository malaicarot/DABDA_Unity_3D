using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class InventoryData
{
    public float itemID;
    public string itemName;
    public int itemQuantity;

    public InventoryData(float id, string name, int quantity)
    {
        itemID = id;
        itemName = name;
        itemQuantity = quantity;

    }
}

[System.Serializable]
public class MapData
{
    public int mapIndex;
    public float xPosionTion;
    public float yPosionTion;
    public float zPosionTion;



    public MapData(int index, float x, float y, float z)
    {
        mapIndex = index;
        xPosionTion = x;
        yPosionTion = y;
        zPosionTion = z;

    }
}

[System.Serializable]
public class SaveData
{
    public List<InventoryData> inventoryDatas;
}

[System.Serializable]
public class CheckPoint
{
    public List<MapData> checkpointDatas;
}

[System.Serializable]
public class CombinedData
{
    public SaveData _saveData;
    public CheckPoint _checkPointData;

    public CombinedData(SaveData saveData, CheckPoint checkPointData)
    {
        this._saveData = saveData;
        this._checkPointData = checkPointData;
    }
}


public class SaveManager : MonoBehaviour
{
    string savePath = "";
    public SaveData saveData;
    public CheckPoint checkPointData;

    public static SaveManager SingletonSaveData;
    void Awake()
    {
        if (SingletonSaveData == null)
        {
            SingletonSaveData = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        savePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
        LoadCombinedData();
    }


    public void SaveCombinedData()
    {
        CombinedData combinedData = new CombinedData(saveData, checkPointData);
        string jsonData = JsonUtility.ToJson(combinedData, true);
        File.WriteAllText(savePath, jsonData);
        Debug.Log(savePath + "Call save data success!");

    }

    public void DeleteFileSave()
    {
        saveData.inventoryDatas.Clear();
        SaveCombinedData();
    }

    public void RemoveItemFromInventory(string name)
    {
        if (saveData == null || saveData.inventoryDatas == null) return;

        for (int i = 0; i < saveData.inventoryDatas.Count; i++)
        {
            if (saveData.inventoryDatas[i].itemName == name)
            {
                saveData.inventoryDatas.RemoveAt(i);
                SaveCombinedData();
                return;
            }
        }
    }

    public void LoadCombinedData()
    {
        if (File.Exists(savePath)) // DA CO FILE
        {
            string jsonData = File.ReadAllText(savePath);
            CombinedData combinedData = JsonUtility.FromJson<CombinedData>(jsonData);
            if (combinedData != null)
            {
                saveData = combinedData._saveData ?? new SaveData
                {
                    inventoryDatas = new List<InventoryData>()
                };
                checkPointData = combinedData._checkPointData ?? new CheckPoint
                {
                    checkpointDatas = new List<MapData>()
                }; ;
            }
            else
            {
                saveData = new SaveData { inventoryDatas = new List<InventoryData>() };
                checkPointData = new CheckPoint { checkpointDatas = new List<MapData>() };


                Debug.Log("Load data Success!");
            }
        }
        else
        {
            saveData = new SaveData
            {
                inventoryDatas = new List<InventoryData>()
            };
            checkPointData = new CheckPoint
            {
                checkpointDatas = new List<MapData>()
            };
            SaveCombinedData();
        }
    }

    public void UpdateInventoryData(float id, string name, int quantity)
    {

        if (saveData == null)
        {
            saveData = new SaveData();
        }
        if (saveData.inventoryDatas == null)
        {
            saveData.inventoryDatas = new List<InventoryData>();
        }

        for (int i = 0; i < saveData.inventoryDatas.Count; i++)
        {
            if (saveData.inventoryDatas[i].itemID == id)
            {
                saveData.inventoryDatas[i].itemQuantity++;
                break;
            }
        }
        InventoryData data = new InventoryData(id, name, quantity);
        saveData.inventoryDatas.Add(data);

        // foreach (var item in saveData.inventoryDatas)
        // {
        //     Debug.Log("Item ID: " + item.itemID);
        //     Debug.Log("Item Name: " + item.itemName);
        //     Debug.Log("Item Quantity: " + item.itemQuantity);
        // }
        SaveCombinedData();
    }

    public void UpdateCheckpointData(int map, float x, float y, float z)
    {

        if (checkPointData == null)
        {
            checkPointData = new CheckPoint();
        }
        if (checkPointData.checkpointDatas == null)
        {
            checkPointData.checkpointDatas = new List<MapData>();
        }
        MapData checkpoint = new MapData(map, x, y, z);
        checkPointData.checkpointDatas.Add(checkpoint);
        foreach (var item in checkPointData.checkpointDatas)
        {
            Debug.Log("map " + item.mapIndex);
            Debug.Log("x " + item.xPosionTion);
            Debug.Log("y " + item.yPosionTion);
            Debug.Log("z " + item.zPosionTion);
        }

        SaveCombinedData();
    }
}
