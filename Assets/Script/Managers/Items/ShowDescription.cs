using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDescription : MonoBehaviour
{
    string itemName;
    InventoryManagers inventoryManagers;
    private void Start()
    {
        inventoryManagers = FindFirstObjectByType<InventoryManagers>();
    }

    public void DisplayDescription()
    {
        Transform img = this.gameObject.transform.Find("Image");
        Image image = img.gameObject.GetComponent<Image>();
        if (image.sprite != null)
        {
            itemName = image.sprite.name;
            inventoryManagers.DisplayDescription(itemName);
        }
        
    }
}
