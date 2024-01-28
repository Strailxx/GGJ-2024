using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel2Script : MonoBehaviour
{
    public InventorySystem inv;

    [Header("Hats")]
    public GameObject Hat;
    public GameObject Hat1;
    public GameObject Hat2;
    public GameObject Hat3;

    private void FixedUpdate()
    {
        HatCheck();
    }
    private void HatCheck()
    {
        if (inv.items.Contains(Hat) || inv.items.Contains(Hat1) || inv.items.Contains(Hat2)|| inv.items.Contains(Hat3))
        {
            Hat.SetActive(false);
            Hat1.SetActive(false);
            Hat2.SetActive(false);
            Hat3.SetActive(false);
        } else
        {
            Hat.SetActive(true);
            Hat1.SetActive(true);
            Hat2.SetActive(true);
            Hat3.SetActive(true);
        }
    }
}
