using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Panel4 : MonoBehaviour
{
    public InventorySystem inv;
    public GameObject shotgun;
    public GameObject peeps0ded;
    public GameObject stardead;
    public GameObject words9;
    public GameObject givegun;
    public bool completedHiddenGoal;
    public bool inPosition;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPosition && peeps0ded)
        {
            inv.Remove(shotgun);
            stardead.SetActive(true);
            words9.SetActive(true);
            givegun.SetActive(true);
            completedHiddenGoal = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inv.items.Contains(shotgun))
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


