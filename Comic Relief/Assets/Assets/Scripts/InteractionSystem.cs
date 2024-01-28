using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters")]
    //DetectionPoint
    public Transform detectionPoint;
    //Detection Radius
    private const float detectionRadius = 0.4f;
    //Detection Layer
    public LayerMask detectionLayer;
    //Cached Trigger
    public GameObject detectedObject;
    
    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();          
            }
        }
    }
    
    //Interaction
    bool InteractInput()
    {
        return Input.GetKeyDown((KeyCode.F));
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }
    
    //Visual Guide for Pickup Area
    
    
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }
    */
}
