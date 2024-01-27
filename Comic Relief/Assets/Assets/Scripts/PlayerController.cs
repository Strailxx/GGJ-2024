using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _mainCamera;
    
    public Transform player;
    
    private Vector2 _moveInput;
    public float moveSpeed = 4;

    public Rigidbody2D rigidBody;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        //_moveInput.y = Input.GetAxisRaw("Vertical");

        _moveInput.Normalize();
        rigidBody.velocity = _moveInput * moveSpeed;
        rigidBody.freezeRotation = true;
    }
}
