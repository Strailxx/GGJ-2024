using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Camera playerCamera;
    public Camera comicCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        comicCamera.enabled = true;
        playerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (comicCamera.enabled)
            {
                comicCamera.enabled = false;
                playerCamera.enabled = true;
            }
            else
            {
                comicCamera.enabled = true;
                playerCamera.enabled = false;
            }
        } 
    }
}
