using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagers : MonoBehaviour
{
    [SerializeField] List<GameObject> inventoryUI; //Game object cha cua hinh anh item
    [SerializeField] List<GameObject> inventorySupportUI;
    [SerializeField] Image inventorySlot;          // Prefab sinh hinh item
    [SerializeField] List<Sprite> itemSprite;      // Hinh anh item

    public List<string> itemNameList;
    public List<string> itemSupportNameList;

    List<Image> imageItemsEmptyList;
    List<Image> imageSupportItemsEmptyList;


    void Start()
    {
        imageItemsEmptyList = new List<Image>();
        imageSupportItemsEmptyList = new List<Image>();

        foreach (GameObject itemSlot in inventoryUI)
        {
            Image slot = Instantiate(inventorySlot, itemSlot.transform);
            imageItemsEmptyList.Add(slot);
            slot.enabled = false;
        }

        foreach (GameObject itemSlot in inventorySupportUI)
        {
            Image slot = Instantiate(inventorySlot, itemSlot.transform);
            imageSupportItemsEmptyList.Add(slot);
            slot.enabled = false;
        }
    }
    public void AddItemsInUI(bool type)
    {
        if (type)
        {
            foreach (Image item in imageSupportItemsEmptyList)
            {
                if (item.sprite == null)
                {
                    item.sprite = itemSprite.Find(item => item.name == itemSupportNameList[itemSupportNameList.Count - 1]);
                    item.enabled = true;
                    break;
                }
            }
        }
        else
        {
            foreach (Image item in imageItemsEmptyList)
            {
                if (item.sprite == null)
                {
                    item.sprite = itemSprite.Find(item => item.name == itemNameList[itemNameList.Count - 1]);
                    item.enabled = true;
                    break;
                }
            }
        }

    }

    public void TargetItem(string name, bool type)
    {
        if (type)
        {
            MarkItem(imageSupportItemsEmptyList, name);

        }
        else
        {
            MarkItem(imageItemsEmptyList, name);

        }

    }

    void MarkItem(List<Image> target, string name)
    {
        foreach (Image item in target)
        {
            Transform parent = item.transform.parent;
            Image parentColor = parent.gameObject.GetComponent<Image>();
            parentColor.color = Color.black;
            if (item.sprite != null)
            {
                if (item.sprite.name == name)
                {
                    parentColor.color = Color.white;
                }
            }
        }
    }
}
