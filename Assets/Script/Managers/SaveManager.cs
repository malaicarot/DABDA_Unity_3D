using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

class InventoryData
{
    public string itemID;
    public string itemName;
    public int itemQuantity;

    public InventoryData(string id, string name, int quantity)
    {
        itemID = id;
        itemName = name;
        itemQuantity = quantity;

    }
}

class SaveData
{
    public List<InventoryData> inventoryDatas;


}
public class SaveManager : MonoBehaviour
{
    string savePath = "";
    SaveData saveData;
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
    }
    void Start()
    {
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
        if(File.Exists(savePath)) // DA CO FILE
        {
            string jsonData = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(jsonData);
            Debug.Log("Load data Success!");
        }
        //  LOAD LAN DAU KH CO FILE
        // Goi lai save data voi gia tri mac dinh

    }

    public void UpdateInventoryData(string id, string name, int quantity)
    {
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
        SaveData();
    }
}
