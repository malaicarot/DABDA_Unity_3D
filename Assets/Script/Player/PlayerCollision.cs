using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    CharacterInput _input;
    PlayerInventory takeItem;
    ItemAbility currentItem;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        _input = GetComponent<CharacterInput>();
        takeItem = GetComponent<PlayerInventory>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && _input.interact)
        {
            animator.SetBool("PickUp", true);
            takeItem.AddInventory(other.gameObject.name);
            Destroy(other.gameObject);
        }

        if(other.CompareTag("ItemInMap") && _input.interact){
            currentItem = AbilityFactory.GetItemAbility(other.name);
            currentItem.Proccess();

        }
        StartCoroutine(SetAnimation());
    }

    IEnumerator SetAnimation(){
        yield return new WaitForSeconds(1);
        animator.SetBool("PickUp", false);

    }
}
