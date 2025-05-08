using UnityEngine;

public class PlayerLoadPosition : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] Vector3 position;
    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerMovement.gameObject.transform.position = position;
    }
}
