using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TrailRenderer trail;

    private float damage;
    private Vector3 direction;
    private float range;
    private Vector3 startPos;
    private Rigidbody2D rb;
    private float speed;
    private int type;
    
    public void Init(Transform pos, float damage, Vector3 direction, float range, float speed, int type)
    {
        this.damage = damage;
        transform.position = pos.position;
        transform.rotation = pos.rotation;
        this.direction = direction;
        this.range = range;
        this.speed = speed;
        this.type = type;
        startPos = transform.position;
        gameObject.SetActive(true);
        trail.Clear();
        trail.emitting = true;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            rb.velocity = direction * speed;

            if(Vector3.Distance(startPos, transform.position) > range)
            {
                ReturnToPool();
            }
        }
    }

    private void ReturnToPool()
    {
        trail.emitting = false;
        gameObject.SetActive(false);
    
        /*
            Type:
                0 = Player Bullet
                1 = Player Laser
                2 = Player Missile
                3 = Enemy Bullet
                4 = Enemy Laser
                5 = Enemy Missile
        */
        
        /*switch(type)
        {
            case 0:
                ProjectilePool.Instance.ReturnPlayerBullet(this.gameObject);
                break;
            
            case 1:
                
                break;
            
            case 2:
                
                break;
            
            case 3:
                
                break;
            
            case 4:
                
                break;
            
            case 5:
                
                break;
        }*/
    }
}
