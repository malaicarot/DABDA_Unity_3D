using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagers : MonoBehaviour
{
    [SerializeField] List<GameObject> inventoryUI; //Game object cha cua hinh anh item
    [SerializeField] Image inventorySlot;          // Prefab sinh hinh item
    [SerializeField] List<Sprite> itemSprite;      // Hinh anh item

    public List<string> itemNameList;

    List<Image> imageItemsEmptyList;

    void Start()
    {
        imageItemsEmptyList = new List<Image>();

        foreach (GameObject itemSlot in inventoryUI)
        {
            Image slot = Instantiate(inventorySlot, itemSlot.transform);
            imageItemsEmptyList.Add(slot);
            slot.enabled = false;
        }

    }
    public void AddItemsInUI()
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

    public void TargetItem(string name)
    {
        foreach (var item in imageItemsEmptyList)
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
