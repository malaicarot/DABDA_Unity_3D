using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayTimeLine : MonoBehaviour
{
    [SerializeField] TimelineController timeline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.PlayTimeline();
        }
    }
}
