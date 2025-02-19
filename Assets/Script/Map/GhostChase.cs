using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<GameObject> ghostList;
    [SerializeField] float speed = 5f;


    void Start()
    {
        ActiveGhost(false);
    }
    void Update()
    {
        if (player != null)
        {

            foreach (GameObject ghost in ghostList)
            {
                Vector3 direction = player.position - ghost.transform.position;
                ghost.transform.position += direction.normalized * speed * Time.deltaTime;
            }
        }
    }


    public void ActiveGhost(bool state)
    {
        foreach (GameObject ghost in ghostList)
        {
            ghost.SetActive(state);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player die!!!!");
        }
    }
}
