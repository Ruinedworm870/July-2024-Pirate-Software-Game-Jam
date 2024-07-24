using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public float health = 100;
    public List<Weapon> weapons = new List<Weapon>();
    
    private Rigidbody2D rb;
    
    private float rotationSpeed = 8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerPosition.playerRb = rb;
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
        
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;
        rb.AddForce(movement, ForceMode2D.Impulse);

        PlayerPosition.pos = transform.position;

        if (Input.GetMouseButton(0))
        {
            foreach (var i in weapons)
            {
                i.Shoot(rb.velocity, WeaponTypes.Laser);
            }
        }

        if(Input.GetMouseButton(1))
        {
            foreach(var i in weapons)
            {
                i.Shoot(rb.velocity, WeaponTypes.Missile);
            }
        }
    }

    private float lastV = 0;
    
    private void LateUpdate()
    {
        float v = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
        
        if (v <= 1f && v < lastV)
        {
            rb.velocity = Vector2.zero;
        }

        lastV = v;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
