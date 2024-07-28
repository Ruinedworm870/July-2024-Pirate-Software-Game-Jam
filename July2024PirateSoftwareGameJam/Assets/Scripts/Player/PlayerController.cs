using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public float shield = 100f;
    public float regenPerMin = 0.1f; //Percentage
    public List<Weapon> weapons = new List<Weapon>();

    public HandleBattleUI handleBattleUI;
    
    private Rigidbody2D rb;
    
    private float rotationSpeed = 8f;

    private float t = 0;
    private float shootMissileDelay = 0.1f;

    private float startHealth;
    private float startShield;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerPosition.playerRb = rb;

        startHealth = health;
        startShield = shield;
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

        if(Input.GetMouseButton(1) && Time.time >= t)
        {
            t = Time.time + shootMissileDelay;
            
            int count = 0;
            bool found = false;
            while(!found && count < weapons.Count)
            {
                found = weapons[count].Shoot(rb.velocity, WeaponTypes.Missile);
                count += 1;
            }
        }
        
        shield = Mathf.Clamp(shield + (startShield * (regenPerMin * (Time.fixedDeltaTime / 60f))), 0, startShield);
        handleBattleUI.UpdateShieldSlider(shield / startShield);

        HandleReloadStatus();
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

    private void HandleReloadStatus()
    {
        int missiles = 0;
        int totalMissiles = 0;
        float lowestMissileReloadTime = 0f;

        int lasers = 0;
        int totalLasers = 0;
        float lowestLaserReloadTime = 0f;
        
        foreach(var i in weapons)
        {
            if(i.weaponType == WeaponTypes.Missile)
            {
                missiles += i.ammo;
                totalMissiles += i.startAmmo;
                
                if(lowestMissileReloadTime == 0 || i.GetReloadTimeRemaining() < lowestMissileReloadTime)
                {
                    lowestMissileReloadTime = i.GetReloadTimeRemaining();
                }
            }
            else
            {
                lasers += i.ammo;
                totalLasers += i.startAmmo;

                if(lowestLaserReloadTime == 0 || i.GetReloadTimeRemaining() < lowestLaserReloadTime)
                {
                    lowestLaserReloadTime = i.GetReloadTimeRemaining();
                }
            }
        }
        
        handleBattleUI.HandleAmmoText(lasers, totalLasers, missiles, totalMissiles, lowestLaserReloadTime, lowestMissileReloadTime);
    }

    public void TakeDamage(float amount, WeaponTypes type)
    {
        if (type == WeaponTypes.Missile)
        {
            health -= amount;
        }
        else
        {
            if (shield > 0)
            {
                shield -= amount;

                if (shield < 0)
                {
                    health += shield;
                }
            }
            else
            {
                health -= amount;
            }
        }

        handleBattleUI.UpdateHealthSliders(health / startHealth, shield / startShield);
        
        if (health <= 0)
        {
            handleBattleUI.OnDeath();
        }
    }
}
