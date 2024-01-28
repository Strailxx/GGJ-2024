using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject currentMenuItem;
    public GameObject player;
    // Objects: 0 = Play || 1 = Options || 2 = Controls
    public GameObject[] objects;
    // Displays 0 = Options || 1 = Controls
    public GameObject[] displays;
    public Slider slider;
    void Start()
    {
        if (slider != null)
        {
            slider.value = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentMenuItem == objects[0])
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (currentMenuItem == objects[1])
            {
                player.SetActive(false);
                displays[0].SetActive(true);
            }
            else if (currentMenuItem == objects[2])
            {
                player.SetActive(false);
                displays[1].SetActive(true);
            }
        }

        if (displays[0] != false && Input.GetKeyDown(KeyCode.Escape))
        {
            displays[0].SetActive(false);
            player.SetActive(true);
        }
        if (displays[1] != false && Input.GetKeyDown(KeyCode.Escape))
        {
            displays[1].SetActive(false);
            player.SetActive(true);
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
