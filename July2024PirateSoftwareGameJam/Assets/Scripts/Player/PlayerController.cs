using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 10f;
    private float rotationSpeed = 8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Gathering mouse pos direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        
        //Rotating to look at mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        
        //Gathering movement input
        float verticalInput = 0f;
        float horizontalInput = 0f;

        if(Input.GetKey(Keybinds.GetKeyCode("D")))
        {
            horizontalInput = 1;
        }
        else if(Input.GetKey(Keybinds.GetKeyCode("A")))
        {
            horizontalInput = -1;
        }

        if (Input.GetKey(Keybinds.GetKeyCode("W")))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(Keybinds.GetKeyCode("S")))
        {
            verticalInput = -1;
        }
        
        Vector2 movement = new Vector2(horizontalInput * Time.fixedDeltaTime, verticalInput * Time.fixedDeltaTime).normalized;
        //transform.position += movement;
        rb.velocity = movement * speed;
    }
}
