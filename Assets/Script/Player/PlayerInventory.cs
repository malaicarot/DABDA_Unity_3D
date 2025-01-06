using System.Collections.Generic;
using UnityEngine;
using System;


enum itemName
{
    Chrysanthemum,
    Lamp
}
[RequireComponent(typeof(PooledObject))]

public class PlayerInventory : PooledObject
{
    [SerializeField] List<GameObject> itemPrefabs;
    MarkPool currentItem;
    ItemAbility currentItemAbility;
    CharacterInput _input;


    void Start()
    {
        _input = GetComponent<CharacterInput>();

    }
    void Update()
    {
        if (currentItemAbility != null && _input.interact)
        {
            currentItemAbility.Proccess();
        }
        SwitchItem(_input.switchItem);
    }

    void EquipItem(string name)
    {
        currentItemAbility = AbilityFactory.GetItemAbility(name);

        if (currentItemAbility != null)
        {
            foreach (var item in itemPrefabs)
            {
                if (item.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    EquipPrefab(name);
                    break;
                }
            }
        }
    }

    void EquipPrefab(string name)
    {
        if (currentItem != null)
        {
            currentItem.ItemRelease();
        }
        currentItem = ItemsPool.SingleTonItemsPool.GetItem(name, transform.position, Quaternion.identity);
    }


    public void SwitchItem(int value)
    {
        switch (value)
        {
            case 1:
                EquipItem(itemName.Chrysanthemum.ToString());
                break;
            case 2:
                EquipItem(itemName.Lamp.ToString());
                break;
            case 3:
                Debug.Log("Item 3");
                break;
            case 4:
                Debug.Log("Item 4");
                break;
            case 5:
                Debug.Log("Item 5");
                break;
        }
    }
}
