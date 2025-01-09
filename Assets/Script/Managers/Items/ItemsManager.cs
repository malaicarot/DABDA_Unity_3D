using TMPro;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI notificationText;
    void Start()
    {
        notificationText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notificationText.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notificationText.enabled = false;
        }
    }


}
