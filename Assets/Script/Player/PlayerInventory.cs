using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

[RequireComponent(typeof(PooledObject))]
public class PlayerInventory : PooledObject
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    MarkPool currentItem;
    MarkPool currentSupportItem;

    ItemAbility currentItemAbility;
    ItemAbility currentSupportItemAbility;

    CharacterInput _input;
    InventoryManagers inventoryManagers;
    Animator animator;
    Dictionary<string, bool> type;
    void Start()
    {
        type = new Dictionary<string, bool>();
        _input = GetComponent<CharacterInput>();
        inventoryManagers = FindFirstObjectByType<InventoryManagers>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (currentItem != null)
        {
            currentItem.transform.position = rightHand.position;
            currentItem.transform.rotation = rightHand.rotation;
        }
        if (currentSupportItem != null)
        {
            currentSupportItem.transform.position = leftHand.position;
            currentSupportItem.transform.rotation = leftHand.rotation;

        }

        if (currentItemAbility != null && Input.GetKeyDown(KeyCode.Q))
        {
            currentItemAbility.Proccess();
        }

        if (currentSupportItemAbility != null && Input.GetKeyDown(KeyCode.R))
        {
            currentSupportItemAbility.Proccess();
        }
        SwitchItem(_input.switchItem);

    }

    void EquipItem(string name, bool type)
    {
        if (type)
        {
            FindItemByType(name, type, inventoryManagers.itemSupportNameList, currentSupportItem, currentSupportItemAbility);
        }
        else
        {
            FindItemByType(name, type, inventoryManagers.itemNameList, currentItem, currentItemAbility);
        }
    }

    void FindItemByType(string name, bool type, List<string> itemList, MarkPool itemType, ItemAbility itemAbility)
    {
        if (itemList.Contains(name))
        {
            if (itemAbility != null)
            {
                foreach (string item in itemList)
                {
                    if (item.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        EquipPrefab(name, type);
                    }
                }
            }
        }
        else
        {
            if (itemType != null)
            {
                itemType.ItemRelease();
            }
        }
    }

    public void AddInventory(string name)
    {
        // animator.SetBool("PickUp", true);
        if (AbilityFactory.GetItemAbility(name).isSupport)
        {
            currentSupportItemAbility = AbilityFactory.GetItemAbility(name);
            if (currentSupportItemAbility != null)
            {
                if (!inventoryManagers.itemSupportNameList.Contains(name))
                {
                    inventoryManagers.itemSupportNameList.Add(name);
                    inventoryManagers.AddItemsInUI(currentSupportItemAbility.isSupport);
                }
            }
            type.Add(name, currentSupportItemAbility.isSupport);
        }
        else
        {
            currentItemAbility = AbilityFactory.GetItemAbility(name);
            if (currentItemAbility != null)
            {
                if (!inventoryManagers.itemNameList.Contains(name))
                {
                    inventoryManagers.itemNameList.Add(name);
                    inventoryManagers.AddItemsInUI(currentItemAbility.isSupport);
                }
            }
            type.Add(name, currentItemAbility.isSupport);
        }
    }

    void EquipPrefab(string name, bool type)
    {
        if (type)
        {
            currentSupportItem = EquipPrefabItemByType(name, currentSupportItem, leftHand);
            animator.SetBool("GrabSupportItems", true);
        }
        else
        {
            currentItem = EquipPrefabItemByType(name, currentItem, rightHand);
            animator.SetBool("GrabItems", true);
        }
    }

    MarkPool EquipPrefabItemByType(string name, MarkPool item, Transform itemPosition)
    {
        if (item != null)
        {
            item.ItemRelease();
        }
        item = ItemsPool.SingleTonItemsPool.GetItem(name, itemPosition.position, itemPosition.rotation);
        item.gameObject.GetComponent<BoxCollider>().enabled = false;
        return item;
    }

    void SwitchItem(int value)
    {
        if (value > 0 && value < 6)
        {
            CheckAndEquipItems(value, inventoryManagers.itemNameList, currentItem);
        }
        else if (value > 5 && value < 10)
        {
            CheckAndEquipItems(value, inventoryManagers.itemSupportNameList, currentSupportItem);
        }
    }

    void CheckAndEquipItems(int value, List<string> itemListBase, MarkPool item)
    {
        int index = 0;
        bool isSupport = false;
        if (value > 0 && value < 6)
        {
            index = value - 1;
            isSupport = false;

        }
        else if (value > 5 && value < 10)
        {
            index = value - 1 - 5;
            isSupport = true;
        }

        if (index >= 0 && index < itemListBase.Count)
        {
            EquipItem(itemListBase[index], type[itemListBase[index]]);
            inventoryManagers.TargetItem(itemListBase[index], type[itemListBase[index]]);
        }
        else
        {
            if (item != null)
            {
                item.ItemRelease();

            }
            if (isSupport)
            {
                animator.SetBool("GrabSupportItems", false);
            }else{
                animator.SetBool("GrabItems", false);

            }
            inventoryManagers.TargetItem("", isSupport);
        }
    }
}
