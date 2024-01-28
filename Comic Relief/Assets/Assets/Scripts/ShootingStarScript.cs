using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarScript : MonoBehaviour
{
    public InventorySystem inv;
    public GameObject star;
    public GameObject shootingStar;

    public GameObject giveGun;
    public GameObject giveHat;

    public GameObject speechNew;

    public bool inPosition;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPosition)
        {
            shootingStar.SetActive(false);
            star.SetActive(true);
            giveGun.SetActive(true);
            giveHat.SetActive(true);
            speechNew.SetActive(true);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
