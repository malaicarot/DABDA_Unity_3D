using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

[RequireComponent(typeof(PooledObject))]
public class PlayerInventory : PooledObject
{
    // [SerializeField] List<string> itemNameList;
    [SerializeField] Transform hand;
    MarkPool currentItem;
    ItemAbility currentItemAbility;
    CharacterInput _input;
    InventoryManagers inventoryManagers;

    enum ItemName
    {
        Chrysanthemum,
        Lamp
    }
    void Start()
    {
        _input = GetComponent<CharacterInput>();
        inventoryManagers = FindFirstObjectByType<InventoryManagers>();
    }
    void Update()
    {
        if (currentItemAbility != null && Input.GetKeyDown(KeyCode.Q))
        {
            currentItemAbility.Proccess();
        }
        SwitchItem(_input.switchItem);
    }

    void EquipItem(string name)
    {
        if (inventoryManagers.itemNameList.Contains(name))
        {
            currentItemAbility = AbilityFactory.GetItemAbility(name);

            if (currentItemAbility != null)
            {
                foreach (string item in inventoryManagers.itemNameList)
                {
                    if (item.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        EquipPrefab(name);
                        // break;
                    }
                }
            }
        }
        else
        {
            if (currentItem != null)
            {
                currentItem.ItemRelease();
            }
        }
    }

    public void AddInventory(string name)
    {
        if (!inventoryManagers.itemNameList.Contains(name))
        {
            inventoryManagers.itemNameList.Add(name);
            inventoryManagers.AddItemsInUI();
        }
    }

    void EquipPrefab(string name)
    {
        if (currentItem != null)
        {
            currentItem.ItemRelease();
        }
        currentItem = ItemsPool.SingleTonItemsPool.GetItem(name, hand.position, Quaternion.identity);
        currentItem.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void SwitchItem(int value)
    {
        CheckAndEquip(value);
        // switch (value)
        // {
        //     case 1:
        //         CheckAndEquip(1);
        //         break;
        //     case 2:
        //         CheckAndEquip(2);
        //         break;
        //     case 3:
        //         CheckAndEquip(3);
        //         break;
        //     case 4:
        //         CheckAndEquip(4);
        //         break;
        //     case 5:
        //         CheckAndEquip(5);
        //         break;
        // }
    }


    void CheckAndEquip(int value)
    {
        int index = value - 1;
        if (index >= 0 && index < inventoryManagers.itemNameList.Count)
        {
            EquipItem(inventoryManagers.itemNameList[index]);
        }
        else
        {
            if (currentItem != null)
            {
                currentItem.Release();
            }
        }
    }
}
