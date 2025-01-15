using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
Instantiate(player, new Vector3(170, 110, 170), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
