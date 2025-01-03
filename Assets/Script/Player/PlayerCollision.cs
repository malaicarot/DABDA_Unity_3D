using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Item")){
            Debug.Log("This is: " + other.name);
        }

    }
}
