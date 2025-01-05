using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<GameObject> itemPrefabs;
    GameObject currentItemPrefabs;
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
    }

    public void EquipItem(string name)
    {
        currentItemAbility = AbilityFactory.GetItemAbility(name);

        if (currentItemAbility != null)
        {
            foreach (var item in itemPrefabs)
            {
                if(item.name.Equals(name, StringComparison.OrdinalIgnoreCase)){
                    EquipPrefab(item);
                    break;
                }
            }
        }
    }

    void EquipPrefab(GameObject prefab){
        if(currentItemPrefabs != null){
            Destroy(currentItemPrefabs);
        }
        currentItemPrefabs = Instantiate(prefab, transform);
        currentItemPrefabs.transform.localPosition = Vector3.zero;

    }
}
