using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject prompt;

    // Get Destination to get the transform destination of the other teleporter
    public Transform GetDestination()
    {
        return destination;
    }

    // Enabling prompt when over a teleporter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (prompt)
            {
                prompt.SetActive(false);
            }
        }
    }
}
