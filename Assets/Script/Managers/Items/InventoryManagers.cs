using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagers : MonoBehaviour
{
    [SerializeField] List<GameObject> inventoryUI; //Game object cha cua hinh anh item
    [SerializeField] List<GameObject> inventorySupportUI;

    /**********************************************************/
    [SerializeField] GameObject SlotInBag; // Prefabs để lưu hình ảnh trong inventory panel
    [SerializeField] GameObject ItemNormalPanel; // Prefabs để nhóm các item bình thường (cha của slot in bag)
    [SerializeField] GameObject ItemSpecialPanel; // Prefabs để nhóm các item đặc biệt (cha của slot in bag)

    /**********************************************************/
    [SerializeField] Image inventorySlot;          // Prefab sinh hinh item
    [SerializeField] List<Sprite> itemSprite;      // Hinh anh item

    [SerializeField] GameObject InventoryUI;
    public List<string> itemNameList;
    public List<string> itemSupportNameList;

    List<Image> imageItemsEmptyList;
    List<Image> imageSupportItemsEmptyList;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    /**********************************************************/
    // Số hàng và cột 
    [SerializeField] int slotInBagColSpecial = 1;
    [SerializeField] int slotInBagRowSpecial = 4;
    [SerializeField] int slotInBagCol = 5;
    [SerializeField] int slotInBagRow = 2;
    // Vị trí tiếp theo
    [SerializeField] float nextPosX = 160;
    [SerializeField] float nextPosY = 160;
    // Vị trí khởi đầu
    [SerializeField] float startPosX = -500;
    [SerializeField] float startPosY = 200;
    [SerializeField] float startPosYSpecial = 200;

    /**********************************************************/

    CharacterInput _input;
    bool inventoryStatus = false;
    List<Image> slotNormal;
    List<Image> slotSpecial;


    void Awake()
    {
        _input = FindFirstObjectByType<CharacterInput>();
        imageItemsEmptyList = new List<Image>();
        imageSupportItemsEmptyList = new List<Image>();
        slotNormal = new List<Image>();
        slotSpecial = new List<Image>();

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
        InventoryUI.SetActive(false);
        SpawnSlotInbag(slotInBagRowSpecial, slotInBagColSpecial, startPosYSpecial, ItemSpecialPanel, slotSpecial);
        SpawnSlotInbag(slotInBagRow, slotInBagCol, startPosY, ItemNormalPanel, slotNormal);
    }
    void Update()
    {
        InventoryHandle();
    }
    public void AddItemsInUI(bool type)
    {
        if (type)
        {
            GetImage(imageSupportItemsEmptyList, itemSupportNameList);// cap nhat UI hien thi ben ngoai (special)
            GetImage(slotSpecial, itemSupportNameList);               // cap nhat UI inventory (special)
        }
        else
        {
            GetImage(imageItemsEmptyList, itemNameList);// cap nhat UI hien thi ben ngoai (normal)
            GetImage(slotNormal, itemNameList);         // cap nhat UI inventory (special)
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


    void SpawnSlotInbag(int row, int col, float startY, GameObject parentPrefab, List<Image> slot)
    {
        for (int i = 0; i < row; i++)
        {
            Vector3 slotPosition = new Vector3(startPosX, startY, 0);
            slotPosition.y -= i * nextPosY;
            for (int j = 0; j < col; j++)
            {
                GameObject slotChild = Instantiate(SlotInBag, slotPosition, Quaternion.identity);
                slotChild.transform.SetParent(parentPrefab.transform, false);
                slotPosition.x += nextPosX;
                Transform child = slotChild.transform.Find("Image");
                Image image = child.gameObject.GetComponent<Image>();
                slot.Add(image);
                image.enabled = false;
            }
        }
    }

    void GetImage(List<Image> list, List<string> itemName)
    {
        foreach (Image item in list)
        {
            if (item.sprite == null)
            {
                item.sprite = itemSprite.Find(item => item.name == itemName[itemName.Count - 1]);
                item.enabled = true;
                break;
            }
        }
    }
    public void RemoveItemUI(string itemName)
    {
        foreach (Image item in imageSupportItemsEmptyList)
        {
            if (item.sprite != null)
            {
               if (item.sprite.name == itemName)
                {
                    item.sprite = null;
                    item.enabled = false;
                    for(int i = 0; i < itemSupportNameList.Count; i++){
                        if(itemSupportNameList[i] == itemName){
                            itemSupportNameList.RemoveAt(i);
                        }
                    }
                    break;
                }
            }
        }
    }


    void InventoryHandle()
    {
        if (_input.getInventory)
        {
            _input.SetCursorState(inventoryStatus);
            inventoryStatus = !inventoryStatus;
            Cursor.visible = inventoryStatus;
            InventoryUI.SetActive(inventoryStatus);
        }
    }


    public void DisplayDescription(string name)
    {
        string content = AbilityFactory.GetItemAbility(name).description;
        if (content != null)
        {
            textMeshProUGUI.text = content;
        }
    }
}
