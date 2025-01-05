using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    PlayerInventory playerInventory;
    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        playerInventory.EquipItem("chrysanthemum");
    }

    public void SwitchItem(int value)
    {
        switch(value){
            case 1:
            Debug.Log("Item 1");
            break;
            case 2:
            Debug.Log("Item 2");
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
