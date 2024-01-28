using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel1Script : MonoBehaviour
{
    public InventorySystem inv;
    public GameObject key;

    public GameObject defaultPanel;
    public GameObject homePanel;

    public GameObject speechDef;
    public GameObject speechNew;

    private void FixedUpdate()
    {
        CheckKey();
    }

    private void CheckKey()
    {
        if (inv.items.Contains(key)) {
            homePanel.SetActive(true);
            defaultPanel.SetActive(false);
            speechDef.SetActive(false);
            speechNew.SetActive(true);
        } else
        {
            homePanel.SetActive(false);
            defaultPanel.SetActive(true);
            speechDef.SetActive(true);
            speechNew.SetActive(false);
        }
    }
}
