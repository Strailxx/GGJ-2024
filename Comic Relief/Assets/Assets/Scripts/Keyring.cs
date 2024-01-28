using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Keyring : MonoBehaviour
{
    public InventorySystem inv;
    public GameObject key;
    public GameObject defaultPanel;
    public GameObject homePanel;
    public GameObject speechDef;
    public GameObject speechNew;
    public bool inPosition;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPosition)
        {
            inv.Remove(key);
            homePanel.SetActive(false);
            defaultPanel.SetActive(true);
            speechDef.SetActive(true);
            speechNew.SetActive(false);
            key.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inv.items.Contains(key))
        {
            inPosition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inPosition = false;
        }
    }
}
