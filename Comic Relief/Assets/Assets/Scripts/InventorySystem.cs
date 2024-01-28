using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    //list
    public List<GameObject> items;
    
    //System Window
    public GameObject uiWindow;
    public Image[] itemsImages;
    
    public void PickUp(GameObject item)
    {
        items.Add(item);
        Update_UI();
    }

    public void Remove(GameObject item)
    {
        items.Remove(item);
        Update_UI();
    }

    void Update_UI()
    {
        HideImages();
        //For each item
        //show in the slots of the ui
        for (int i = 0; i < items.Count; i++)
        {
            itemsImages[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            itemsImages[i].gameObject.SetActive(true);
        }
    }

    void HideImages()
    {
        foreach (var i in itemsImages) { i.gameObject.SetActive(false); }
    }
}
