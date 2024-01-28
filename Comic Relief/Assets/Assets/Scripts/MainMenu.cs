using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject currentMenuItem;
    public GameObject play;
    public GameObject controls;
    public GameObject options;
    
    //
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentMenuItem == play)
            {
                Debug.Log("Play");
            }
            else if (currentMenuItem == controls)
            {
                Debug.Log("Controls");
            }
            else if (currentMenuItem == options)
            {
                Debug.Log("Options");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentMenuItem = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentMenuItem = null;
        }
    }
}
