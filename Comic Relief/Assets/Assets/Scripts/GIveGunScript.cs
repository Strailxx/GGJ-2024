using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIveGunScript : MonoBehaviour
{
    public InventorySystem inv;
    public GameObject Gun;

    public GameObject peep0;
    public GameObject deadPeep0;

    public GameObject DestGun;

    public bool starHasGun = false;

    public bool inPosition;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPosition)
        {
            DestGun.SetActive(true);
            inv.Remove(Gun);
            peep0.SetActive(false);
            deadPeep0.SetActive(true);
            starHasGun = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inv.items.Contains(Gun))
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
