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
public class SaveData
{
    public List<InventoryData> inventoryDatas;


}
public class SaveManager : MonoBehaviour
{
    string savePath = "";
    public SaveData saveData;
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
        LoadData();
    }


    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, jsonData);
        Debug.Log(savePath + "Call save data success!");

    }

    public void LoadData()
    {
        if (File.Exists(savePath)) // DA CO FILE
        {
            string jsonData = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(jsonData);
            Debug.Log("Load data Success!");
        }
        else
        {
            saveData = new SaveData
            {
                inventoryDatas = new List<InventoryData>()
            };
            SaveData();
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

        foreach (var item in saveData.inventoryDatas)
        {
            Debug.Log("Item ID: " + item.itemID);
            Debug.Log("Item Name: " + item.itemName);
            Debug.Log("Item Quantity: " + item.itemQuantity);
        }
        SaveData();
    }
}
