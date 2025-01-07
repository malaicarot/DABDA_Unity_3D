using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    CharacterInput _input;
    PlayerInventory takeItem;
    private void Start()
    {
        _input = GetComponent<CharacterInput>();
        takeItem = GetComponent<PlayerInventory>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && _input.interact)
        {
            takeItem.AddInventory(other.gameObject.name);
            Debug.Log(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
