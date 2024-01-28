using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHatScript : MonoBehaviour
{
    public InventorySystem inv;
    public GameObject Hat;
    public GameObject Hat1;
    public GameObject Hat2;
    public GameObject Hat3;

    public GameObject DestHat;

    public bool StarHasHat = false;

    public bool inPosition;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPosition)
        {
            DestHat.SetActive(true);
            inv.Remove(Hat);
            inv.Remove(Hat1);
            inv.Remove(Hat2);
            inv.Remove(Hat3);
            Hat.SetActive(false);
            Hat1.SetActive(false);
            Hat2.SetActive(false);
            Hat3.SetActive(false);
            StarHasHat = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inv.items.Contains(Hat) || inv.items.Contains(Hat1) || inv.items.Contains(Hat2) || inv.items.Contains(Hat3))
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
