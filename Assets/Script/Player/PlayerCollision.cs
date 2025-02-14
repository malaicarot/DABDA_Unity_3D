using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerMovement playerMovement;
    CharacterInput _input;
    PlayerInventory takeItem;
    ItemAbility currentItem;
    Animator animator;
    LoadScene loadScene;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        _input = GetComponent<CharacterInput>();
        takeItem = GetComponent<PlayerInventory>();
        loadScene = FindFirstObjectByType<LoadScene>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && _input.interact)
        {
            animator.SetBool("PickUp", true);
            string name = other.gameObject.name;
            string correctName = name.Substring(0, name.Length - 6);
            takeItem.AddInventory(correctName);
            SaveManager.SingletonSaveData.UpdateInventoryData(other.gameObject.transform.GetInstanceID(), correctName, 1);
            SoundSingleton._instance.GetItem();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("ItemInMap") && _input.interact)
        {
            currentItem = AbilityFactory.GetItemAbility(other.name);
            currentItem.Proccess();

        }

        if (other.CompareTag("TriggerLimit"))
        {
            playerMovement.isLimitJump = true;

        }
        StartCoroutine(SetAnimation());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerLimit"))
        {
            playerMovement.isLimitJump = false;

        }
    }

    IEnumerator SetAnimation()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("PickUp", false);

    }
}
