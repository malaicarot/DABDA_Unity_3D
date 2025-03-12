using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSystem : MonoBehaviour
{
    [SerializeField] float force = 20f;
    void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Torus"))
            {
                playerMovement.ApplyForce(force);
            }
            else
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                playerMovement.ApplyForceKnockBack(knockbackDir);
            }
        }
    }
}
