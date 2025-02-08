using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSystem : MonoBehaviour
{
    [SerializeField] float force = 20f;
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.ApplyForce(force);
        }
    }
}
