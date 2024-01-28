using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//forces item to have box collider with correct size
[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType { NONE,Interact,PickUp}
    public InteractionType type;
    public List<GameObject> items;
    //Forces items to have a trigger
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 6;
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractionType.PickUp:
                //Add item to inventory System
                Debug.Log("Pickup");
                if (FindObjectOfType<InventorySystem>().items.Count < 2)
                {
                    FindObjectOfType<InventorySystem>().PickUp(gameObject);
                    gameObject.SetActive(false);
                    Debug.Log("Innv no full");
                }
                break;
            case InteractionType.Interact:
                Debug.Log("Interact");
                break;
            
            default:
                Debug.Log("NULL");
                break;
        }
    }
}
