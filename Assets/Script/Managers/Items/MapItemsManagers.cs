using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemsManagers : MonoBehaviour
{
    ItemAbility currentItem;

    void OnTriggerEnter(Collider other) {
        currentItem = AbilityFactory.GetItemAbility(this.name);
        if (other.CompareTag("Player"))
        {
            currentItem.Proccess();
        }
    }
}
