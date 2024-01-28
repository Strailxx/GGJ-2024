using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportation : MonoBehaviour
{
    private GameObject currentTeleporter;

    // Updates every frame to check for F input if F is inputted teleport player to other teleporter set within the public vars
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleportation>().GetDestination().position;
            }
        }
    }

    // Setting the current teleporter for use up above when walking over at teleporter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            currentTeleporter = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            if (other.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
